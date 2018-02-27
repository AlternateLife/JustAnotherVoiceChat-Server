using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Server
{
    public partial class RagempVoiceServer
    {
        
        public new event Delegates<IRagempVoiceClient>.EmptyEvent OnServerStarted;
        public new event Delegates<IRagempVoiceClient>.EmptyEvent OnServerStopping;

        private void AttachToEvents()
        {
            base.OnServerStarted += OnVoiceServerStarted;
            base.OnServerStopping += OnVoiceServerStopping;
        }
        
        private void OnVoiceServerStarted()
        {
            foreach (var player in NAPI.Pools.GetAllPlayers())
            {
                RegisterPlayer(player);
            }
            
            OnServerStarted?.Invoke();
        }

        private void OnVoiceServerStopping()
        {
            foreach (var player in NAPI.Pools.GetAllPlayers())
            {
                RegisterPlayer(player);
            }
            
            OnServerStarted?.Invoke();
        }

        public void BridgeOnClientConnectedEvent(Client client)
        {
            if (RegisterPlayer(client) != null)
            {
                return;
            }

            client.Kick("Failed to start VoiceClient!");
            NAPI.Util.ConsoleOutput($"Failed to start VoiceClient for Client {client.Name}");
        }

        public void BridgeOnClientDisconnectedEvent(Client client)
        {
            UnregisterPlayer(client);
        }
    }
}
