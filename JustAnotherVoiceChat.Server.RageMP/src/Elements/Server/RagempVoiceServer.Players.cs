using System;
using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Server
{
    public partial class RagempVoiceServer
    {

        private const string VoiceClientInstance = "JV_INSTANCE";
        
        public IRagempVoiceClient GetVoiceClient(Client player)
        {
            if (player.HasData(VoiceClientInstance))
            {
                return (IRagempVoiceClient) player.GetData(VoiceClientInstance);
            }

            return FindClient(c => c.Player == player);
        }
        
        private IRagempVoiceClient RegisterPlayer(Client player)
        {
            var voiceClient = PrepareClient(player);
            if (voiceClient == null)
            {
                return null;
            }
            
            player.SetData(VoiceClientInstance, voiceClient);
            
            OnClientPrepared?.Invoke(voiceClient);
            
            return voiceClient;
        }

        private void UnregisterPlayer(Client player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            var client = GetVoiceClient(player);
            if (client != null)
            {
                RemoveClient(client);
                client.Player.ResetData(VoiceClientInstance);
            }
        }

    }
}
