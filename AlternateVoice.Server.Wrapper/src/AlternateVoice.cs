using AlternateVoice.Server.Wrapper.Elements;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper
{
    public static class AlternateVoice
    {

        public static IVoiceServer Server { get; private set; }
        
        public static IVoiceServer CreateServer(string hostname, ushort port, int channelId)
        {
            if (Server == null)
            {
                return null;
            }
            
            Server = new VoiceServer(hostname, port, channelId);

            return Server;
        }
        
        
    }
}
