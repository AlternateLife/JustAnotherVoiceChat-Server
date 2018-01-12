using AlternateVoice.Server.Wrapper.Enums;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper
{
    public class Delegates
    {
        public delegate void EmptyEvent();
        
        public delegate void ClientEvent(IVoiceClient client);
        public delegate void ClientDisconnected(IVoiceClient client, DisconnectReason reason);

        public delegate void ClientGroupEvent(IVoiceClient client, IVoiceGroup group);
    }
}
