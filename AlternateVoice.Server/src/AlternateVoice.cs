using AlternateVoice.Server.Interfaces;
using AlternateVoice.Server.Managers;

namespace AlternateVoice.Server
{
    public static class AlternateVoice
    {

        private static IVoiceServer _voiceServer;

        public static IVoiceServer Server
        {
            get
            {
                if (_voiceServer == null)
                {
                    _voiceServer = new VoiceServer();
                }
                
                return _voiceServer;
            } 
        }
        
    }
}
