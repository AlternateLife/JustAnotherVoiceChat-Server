using AlternateVoice.Server.Dummy.Client;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Dummy.Repositories
{
    public class ClientRepository : IVoiceClientRepository
    {
        public IVoiceClient MakeClient(IVoiceServer server, VoiceHandle handle, params object[] arguments)
        {
            return new DummyClient(server, handle);
        }
    }
}
