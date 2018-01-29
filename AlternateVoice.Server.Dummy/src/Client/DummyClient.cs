using AlternateVoice.Server.Wrapper.Elements.Client;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Math;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Dummy.Client
{
    public class DummyClient : VoiceClient
    {
        private Vector3 _position;
        public override Vector3 Position => _position;

        public DummyClient(IVoiceServer server, VoiceHandle handle) : base(server, handle)
        {
            
        }

    }
}
