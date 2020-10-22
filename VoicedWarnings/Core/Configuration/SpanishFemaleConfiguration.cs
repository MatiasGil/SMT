namespace SMT.VoicedWarnings.Core.Configuration
{
    public class SpanishFemaleConfiguration : VoiceConfiguration
    {
        private readonly string Language;
        private readonly VoiceGender Gender;
        private readonly string Name;

        public SpanishFemaleConfiguration()
        {
            Language = "es-ES";
            Gender = Configuration.VoiceGender.FEMALE;
            Name = "es-ES-Standard-A";
        }

        public string SpeechLanguage()
        {
            return Language;
        }
        
        public VoiceGender VoiceGender()
        {
            return Gender;
        }

        public string VoiceName()
        {
            return Name;
        }

        public string GetTextToSpeak(int jumpsAway)
        {
            if (jumpsAway == 0)
            {
                return "Hostíl en tu systema!";
            }
            else if (jumpsAway == 1)
            {
                return "Hostil en el systema vecino!";
            }

            return "Hostil a " + jumpsAway + "saltos!";
        }
    }
}