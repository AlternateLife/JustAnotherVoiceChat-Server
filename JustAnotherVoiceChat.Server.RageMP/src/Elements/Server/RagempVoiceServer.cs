using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Server
{
    public partial class RagempVoiceServer : VoiceServer<IRagempVoiceClient, Client>, IRagempVoiceServer
    {
        
        public event RagempVoiceDelegates.RagempVoiceClientEvent OnClientPrepared;
        
        protected RagempVoiceServer(IVoiceClientFactory<IRagempVoiceClient, Client> factory, VoiceServerConfiguration configuration) : base(factory, configuration)
        {
            AttachToEvents();
        }
    }
}
