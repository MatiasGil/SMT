using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using Google.Cloud.TextToSpeech.V1;
using SMT.VoicedWarnings.Core.Configuration;

namespace SMT.VoicedWarnings.Core.Services
{
    public class GoogleTextToSpeechService: VoiceService
    {
        private readonly VoiceConfiguration voiceConfiguration;
        private readonly TextToSpeechClient client;

        private Dictionary<string, MediaPlayer> sounds = new Dictionary<string, MediaPlayer>();
        public GoogleTextToSpeechService(VoiceConfiguration voiceConfiguration)
        {
            this.voiceConfiguration = voiceConfiguration;
            client = TextToSpeechClient.Create();

            LoadAllAudioFiles();
        }

        private void LoadAllAudioFiles()
        {
            sounds = new Dictionary<string, MediaPlayer>();
            for (int i = 0; i < 9; i++)
            {
                string fileName = GetFileNameFor(i);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\" + fileName))
                {
                    sounds.Add(fileName, CreateMediaPlayerFromAudio(fileName));
                }
            }
        }

        public void Warn(int amountOfJumps)
        {
            string fileName = GetFileNameFor(amountOfJumps);

            if (!sounds.ContainsKey(fileName))
            {
                CreateAudioFromSpeech(amountOfJumps, fileName);
            }
            
            PlaySound(fileName);
        }

        private string GetFileNameFor(int amountOfJumps)
        {
            return amountOfJumps + "-" + voiceConfiguration.SpeechLanguage() + "-" + voiceConfiguration.VoiceGender() + ".mp3";
        }

        private void CreateAudioFromSpeech(int amountOfJumps, string fileName)
        {
            // Set the text input to be synthesized.
            SynthesisInput input = new SynthesisInput
            {
                Text = voiceConfiguration.GetTextToSpeak(amountOfJumps)
            };

            // Build the voice request, select the language code ("en-US"),
            // and the SSML voice gender ("neutral").
            VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = voiceConfiguration.SpeechLanguage(),
                SsmlGender = MapToGoogleGender(voiceConfiguration.VoiceGender()),
                Name = voiceConfiguration.VoiceName()
            };

            // Select the type of audio file you want returned.
            AudioConfig config = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3,
                SpeakingRate = 1.25
            };

            // Perform the Text-to-Speech request, passing the text input
            // with the selected voice parameters and audio file type
            var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
            {
                Input = input,
                Voice = voice,
                AudioConfig = config
            });


            // Write the binary AudioContent of the response to an MP3 file.
            using (Stream output = File.Create(AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\" + fileName))
            {
                response.AudioContent.WriteTo(output);
                sounds.Add(fileName, CreateMediaPlayerFromAudio(fileName));
            }
        }

        private static MediaPlayer CreateMediaPlayerFromAudio(string fileName)
        {
            var mediaPlayer = new MediaPlayer();
            Uri woopUri = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Sounds\" + fileName);
            mediaPlayer.Open(woopUri);
            return mediaPlayer;
        }

        private void PlaySound(string fileName)
        {
            sounds[fileName].Stop();
            sounds[fileName].Position = new TimeSpan(0, 0, 0);
            sounds[fileName].Play();
        }

        private SsmlVoiceGender MapToGoogleGender(VoiceGender voiceGender)
        {
            switch (voiceGender)
            {
                case VoiceGender.MALE:
                    return SsmlVoiceGender.Male;
                case VoiceGender.FEMALE:
                    return SsmlVoiceGender.Female;
                case VoiceGender.NEUTRAL:
                    return SsmlVoiceGender.Neutral;
                case VoiceGender.UNSPECIFIED:
                    return SsmlVoiceGender.Unspecified;
                default:
                    return SsmlVoiceGender.Neutral;
            }
        }
    }
}