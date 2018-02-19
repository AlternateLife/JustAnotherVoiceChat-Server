using JustAnotherVoiceChat.Server.Wrapper.Elements.Client;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceClient : VoiceClient<FakeVoiceClient>
    {
        public byte Identifer { get; }

        public FakeVoiceClient(byte identifer, IVoiceServer<FakeVoiceClient> server, VoiceHandle handle) : base(server, handle)
        {
            Identifer = identifer;
        }

        public override Vector3 Position { get; } = new Vector3();
    }
}
