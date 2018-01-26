using AlternateVoice.Server.Wrapper;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Server
{
    public partial class GtmpVoiceServer
    {

        public event Delegates.EmptyEvent OnServerStarted;
        public event Delegates.EmptyEvent OnServerStopping;
        
        public void AttachToEvents()
        {
            _api.onPlayerFinishedDownload += OnPlayerConnect;
            _api.onPlayerDisconnected += OnPlayerDisconnect;

            _server.OnServerStarted += OnServerStarted;
            _server.OnServerStopping += OnServerStopping;
        }

        private void OnPlayerConnect(Client player)
        {
            
        }

        private void OnPlayerDisconnect(Client player, string reason)
        {
        
        }
    }
}
