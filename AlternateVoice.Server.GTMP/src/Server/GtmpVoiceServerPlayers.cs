using System;
using System.Collections.Concurrent;
using AlternateVoice.Server.GTMP.Clients;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace AlternateVoice.Server.GTMP.Server
{
    public partial class GtmpVoiceServer
    {
        
        private readonly ConcurrentDictionary<NetHandle, IGtmpVoiceClient> _clients = new ConcurrentDictionary<NetHandle, IGtmpVoiceClient>();
        
        public IGtmpVoiceClient GetVoiceClientOfPlayer(Client player)
        {
            IGtmpVoiceClient result;
            if (_clients.TryGetValue(player.handle, out result))
            {
                return result;
            }

            return null;
        }

        private IGtmpVoiceClient RegisterPlayer(Client player)
        {
            var client = new GtmpVoiceClient(player);

            if (_clients.TryAdd(player.handle, client))
            {
                return client;
            }
            
            return null;
        }

        private void UnregisterPlayer(Client player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            IGtmpVoiceClient removedClient;
            if (_clients.TryRemove(player.handle, out removedClient))
            {
                
            }
        }
        
    }
}
