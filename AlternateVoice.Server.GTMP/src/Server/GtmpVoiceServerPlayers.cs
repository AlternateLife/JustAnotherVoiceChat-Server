using System.Collections.Concurrent;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace AlternateVoice.Server.GTMP.Server
{
    public partial class GtmpVoiceServer
    {
        
        private ConcurrentDictionary<NetHandle, IGtmpVoiceClient> _clients;
        
        public IGtmpVoiceClient GetVoiceClientOfPlayer(Client player)
        {
            IGtmpVoiceClient result;
            if (_clients.TryGetValue(player.handle, out result))
            {
                return result;
            }

            return null;
        }
        
    }
}
