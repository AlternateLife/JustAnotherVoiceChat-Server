using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Server
{
    public class RagempVoiceServer : VoiceServer<IRagempVoiceClient, Client>, IRagempVoiceServer
    {
        protected RagempVoiceServer(IVoiceClientFactory<IRagempVoiceClient, Client> factory, VoiceServerConfiguration configuration) : base(factory, configuration)
        {
            
            
            
        }
    }
}
