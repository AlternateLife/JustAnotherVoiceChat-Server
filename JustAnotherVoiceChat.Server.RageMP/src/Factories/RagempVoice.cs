using JustAnotherVoiceChat.Server.RageMP.Elements.Server;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;

namespace JustAnotherVoiceChat.Server.RageMP.Factories
{
    public static class RagempVoice
    {
        
        private static IRagempVoiceServer _shared;
        public static IRagempVoiceServer Shared
        {
            get => _shared;
            set
            {
                if (_shared != null)
                {
                    return;
                }

                _shared = value;
            }
        }
        
        public static IRagempVoiceServer CreateServer(VoiceServerConfiguration configuration, bool setShared = true)
        {
            if (Shared != null)
            {
                return Shared;
            }

            var server = new RagempVoiceServer(new RagempVoiceClientFactory(), configuration);
            if (setShared)
            {
                Shared = server;
            }

            return server;
        }
        
    }
}
