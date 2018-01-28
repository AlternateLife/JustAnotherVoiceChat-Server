using System;
using System.Collections.Concurrent;
using System.Linq;
using AlternateVoice.Server.GTMP.Clients;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace AlternateVoice.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer
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

        public void SetCameraRotationOfPlayer(Client player, float cameraRotation)
        {
            var client = GetVoiceClientOfPlayer(player);

            client?.SetCameraRotation(cameraRotation);
        }

        private IGtmpVoiceClient RegisterPlayer(Client player)
        {
            var voiceClient = _server.CreateClient();
            if (voiceClient == null)
            {
                return null;
            }
            
            var client = new GtmpVoiceClient(player, voiceClient);
            if (!_clients.TryAdd(player.handle, client))
            {
                return null;
            }
            
            OnClientPrepared?.Invoke(client);
            
            return client;
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
                var voiceClient = removedClient as GtmpVoiceClient;
                voiceClient?.Dispose();
            }
        }
        
    }
}
