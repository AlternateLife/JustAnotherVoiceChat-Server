using AlternateVoice.Server.Wrapper.Elements.Client;
using AlternateVoice.Server.Wrapper.Enums;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        public void TriggerClientConnectedEvent(ushort handle)
        {
            VoiceClient client;
            if (_clients.TryGetValue(handle, out client))
            {
                OnClientConnecting?.Invoke(client);
            }
        }

        public void TriggerClientDisconnectedEvent(ushort handle)
        {
            VoiceClient client;
            if (_clients.TryGetValue(handle, out client))
            {
                OnClientDisconnected?.Invoke(client, DisconnectReason.None);
            }
        }
    }
}
