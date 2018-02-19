using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceClientFactory : IVoiceClientFactory<IFakeVoiceClient, byte>
    {
        public IFakeVoiceClient MakeClient(byte identifier, IVoiceServer<IFakeVoiceClient> server, VoiceHandle handle)
        {
            return new FakeVoiceClient(identifier, server, handle);
        }
    }
}
