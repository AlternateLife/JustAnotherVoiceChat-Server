using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Elements.Clients;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.RageMP.Factories
{
    public class RagempVoiceClientFactory : IRagempVoiceClientFactory
    {
        public IRagempVoiceClient MakeClient(Client identifier, IVoiceServer<IRagempVoiceClient> server, VoiceHandle handle)
        {
            return new RagempVoiceClient(identifier, server, handle);
        }
    }
}
