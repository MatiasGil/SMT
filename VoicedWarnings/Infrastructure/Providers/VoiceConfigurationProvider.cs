using SMT.VoicedWarnings.Core.Configuration;

namespace SMT.VoicedWarnings.Infrastructure.Providers
{
    public static class VoiceConfigurationProvider
    {
        private static VoiceConfiguration voiceConfiguration = null;
        public static VoiceConfiguration ProvideVoiceConfiguration()
        {
            if(voiceConfiguration == null)
                voiceConfiguration = new SpanishFemaleConfiguration();

            return voiceConfiguration;
        }
    }
}