using AlternateVoice.Server.GTMP.Clients;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Repositories
{
    public class ClientRepository : IVoiceClientRepository
    {
        public IVoiceClient MakeClient(IVoiceServer server, VoiceHandle handle, params object[] arguments)
        {
            return new GtmpVoiceClient((Client) arguments[0], server, handle);
        }
    }
}
