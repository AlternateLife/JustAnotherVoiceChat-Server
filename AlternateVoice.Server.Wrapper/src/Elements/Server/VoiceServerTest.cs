namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        public void TriggerClientConnectedEvent(ushort handle)
        {
            OnClientConnectedFromVoice(handle);
        }

        public void TriggerClientDisconnectedEvent(ushort handle)
        {
            OnClientDisconnectedFromVoice(handle);
        }
    }
}
