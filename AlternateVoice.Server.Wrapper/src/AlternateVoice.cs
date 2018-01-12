using AlternateVoice.Server.Elements;
using AlternateVoice.Server.Interfaces;

namespace AlternateVoice.Server
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
