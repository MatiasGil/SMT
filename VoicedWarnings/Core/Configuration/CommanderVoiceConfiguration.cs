namespace SMT.VoicedWarnings.Core.Configuration
{
    public class CommanderVoiceConfiguration : VoiceConfiguration
    {
        private readonly string Language;
        private readonly VoiceGender Gender;

        public CommanderVoiceConfiguration()
        {
            Language = "us-EN";
            Gender = Configuration.VoiceGender.FEMALE;
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
            return "";
        }

        public string GetTextToSpeak(int jumpsAway)
        {
            if (jumpsAway == 0)
            {
                return "Threat in your system Commander!";
            }

            return "Warning Commander, threat at " + jumpsAway + "Jumps Away";
        }
    }
}