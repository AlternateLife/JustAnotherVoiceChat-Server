using GTANetworkAPI;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Interfaces
{
    public interface IRagempVoiceServer : IVoiceServer<IRagempVoiceClient>
    {

        event RagempVoiceDelegates.RagempVoiceClientEvent OnClientPrepared;
        new event Delegates<IRagempVoiceClient>.EmptyEvent OnServerStarted;
        new event Delegates<IRagempVoiceClient>.EmptyEvent OnServerStopping;
        
        IRagempVoiceClient GetVoiceClient(Client player);

        void BridgeOnClientConnectedEvent(Client client);
        void BridgeOnClientDisconnectedEvent(Client client);

    }
}
