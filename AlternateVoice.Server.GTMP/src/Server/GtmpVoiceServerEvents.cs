using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Server
{
    public partial class GtmpVoiceServer
    {
        public void AttachToEvents()
        {
            _api.onPlayerFinishedDownload += OnPlayerConnect;
            _api.onPlayerDisconnected += OnPlayerDisconnect;
        }

        private void OnPlayerConnect(Client player)
        {
            
        }

        private void OnPlayerDisconnect(Client player, string reason)
        {
        
        }
        
    }
}
