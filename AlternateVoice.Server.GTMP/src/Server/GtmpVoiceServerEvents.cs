using System.Linq;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer
    {

        public event Delegates.EmptyEvent OnServerStarted;
        public event Delegates.EmptyEvent OnServerStopping;
        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientPrepared;

        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnPlayerStartsTalking;
        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnPlayerStopsTalking;

        private void AttachToEvents()
        {
            _api.onPlayerFinishedDownload += OnPlayerConnect;
            _api.onPlayerDisconnected += OnPlayerDisconnect;

            _server.OnServerStarted += OnVoiceServerStarted;
            _server.OnServerStopping += OnVoiceServerStopping;

            _server.OnClientStartsTalking += OnClientStartsTalking;
            _server.OnClientStopsTalking += OnClientStopsTalking;
        }

        private void OnVoiceServerStarted()
        {
            foreach (var player in _api.getAllPlayers())
            {
                RegisterPlayer(player);
            }
            
            OnServerStarted?.Invoke();
        }

        private void OnVoiceServerStopping()
        {
            foreach (var client in _clients.Values.ToArray())
            {
                UnregisterPlayer(client.Player);
            }
            
            OnServerStopping?.Invoke();
        }

        private void OnPlayerConnect(Client player)
        {
            if (RegisterPlayer(player) != null)
            {
                return;
            }

            player.kick("Failed to start VoiceClient!");
            _api.consoleOutput(LogCat.Error, $"Failed to start VoiceClient for Client {player.name}");
        }

        private void OnPlayerDisconnect(Client player, string reason)
        {
            UnregisterPlayer(player);
        }

        private void OnClientStartsTalking(IVoiceClient client)
        {
            var gtmpVoiceClient  = GetVoiceClient(client);
            OnPlayerStartsTalking?.Invoke(gtmpVoiceClient);
        }

        private void OnClientStopsTalking(IVoiceClient client)
        {
            var gtmpVoiceClient = GetVoiceClient(client);
            OnPlayerStopsTalking?.Invoke(gtmpVoiceClient);
        }

        public void TestLipSyncActiveForPlayer(Client player)
        {
            IGtmpVoiceClient result;
            if (_clients.TryGetValue(player.handle, out result))
            {
                _server.TestLipSyncActiveForClient(result.VoiceClient);
            }
        }

        public void TestLipSyncInactiveForPlayer(Client player)
        {
            IGtmpVoiceClient result;
            if (_clients.TryGetValue(player.handle, out result))
            {
                _server.TestLipSyncActiveForClient(result.VoiceClient);
            }
        }
    }
}
