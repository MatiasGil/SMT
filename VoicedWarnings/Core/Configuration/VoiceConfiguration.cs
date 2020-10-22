namespace SMT.VoicedWarnings.Core.Configuration
{
    public interface VoiceConfiguration
    {
        string SpeechLanguage();
        VoiceGender VoiceGender();
        string VoiceName();
        string GetTextToSpeak(int jumpsAway);
    }
    
    public enum VoiceGender
    {
        MALE,
        FEMALE,
        NEUTRAL,
        UNSPECIFIED
    }
}