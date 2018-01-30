using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        
        public void FireClientStartsSpeaking(IVoiceClient client)
        {
            OnClientStartsTalking?.Invoke(client);
        }

        public void FireClientStopsSpeaking(IVoiceClient client)
        {
            OnClientStopsTalking?.Invoke(client);
        }
        
        public void FireClientConnected(ushort handle)
        {
            AVTest_CallNewClientCallback(handle);
        }

        public void FireClientDisconnected(ushort handle)
        {
            OnClientDisconnectedFromVoice(handle);
        }
        
    }
}
