using JustAnotherVoiceChat.Server.Wrapper.Elements.Client;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceClient : VoiceClient
    {
        public byte Identifer { get; }

        public FakeVoiceClient(byte identifer, IVoiceServer server, IVoiceWrapper3D voiceWrapper3D, VoiceHandle handle) : base(server, voiceWrapper3D, handle)
        {
            Identifer = identifer;
        }

        public override Vector3 Position { get; } = new Vector3();
    }
}
