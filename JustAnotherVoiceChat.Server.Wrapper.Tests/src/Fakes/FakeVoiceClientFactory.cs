using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceClientFactory : IVoiceClientFactory<FakeVoiceClient, byte>
    {
        public FakeVoiceClient MakeClient(byte identifier, IVoiceServer<FakeVoiceClient> server, VoiceHandle handle)
        {
            return new FakeVoiceClient(identifier, server, handle);
        }
    }
}
