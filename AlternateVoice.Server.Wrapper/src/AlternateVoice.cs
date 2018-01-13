using AlternateVoice.Server.Wrapper.Elements.VoiceServerParts;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper
{
    public static class AlternateVoice
    {

        public static IVoiceServer Server { get; private set; }
        
        public static IVoiceServer MakeAndStoreServer(string hostname, ushort port, int channelId)
        {
            if (Server == null)
            {
                return null;
            }
            
            Server = MakeServer(hostname, port, channelId);

            return Server;
        }

        public static IVoiceServer MakeServer(string hostname, ushort port, int channelId)
        {
            return new VoiceServer(hostname, port, channelId);
        }


    }
}
