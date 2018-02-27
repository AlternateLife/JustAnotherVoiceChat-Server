using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Enums;

namespace JustAnotherVoiceChat.Server.RageMP
{
    public static class RagempVoiceDelegates
    {
        
        public delegate void RagempVoiceClientEvent(IRagempVoiceClient client);
        public delegate void RagempVoiceClientStatusEvent(IRagempVoiceClient client, bool newStatus);

        public delegate void RagempVoiceLogMessageEvent(LogLevel logLevel, string message);
        
    }
}
