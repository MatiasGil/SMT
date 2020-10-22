using SMT.VoicedWarnings.Core.Services;

namespace SMT.VoicedWarnings.Infrastructure.Providers
{
    public static class VoiceServicesProvider
    {
        private static VoiceService voiceServiceInstance = null;
        
        public static VoiceService ProvideVoiceService()
        {
            if(voiceServiceInstance == null)
                voiceServiceInstance = new GoogleTextToSpeechService(VoiceConfigurationProvider.ProvideVoiceConfiguration());

            return voiceServiceInstance;
        }
    }
}