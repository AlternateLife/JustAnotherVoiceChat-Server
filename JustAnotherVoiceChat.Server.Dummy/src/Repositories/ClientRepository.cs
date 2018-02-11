using JustAnotherVoiceChat.Server.Dummy.Client;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Dummy.Repositories
{
    public class ClientRepository : IVoiceClientRepository
    {
        public IVoiceClient MakeClient(IVoiceServer server, VoiceHandle handle, params object[] arguments)
        {
            return new DummyClient(server, handle);
        }
    }
}
