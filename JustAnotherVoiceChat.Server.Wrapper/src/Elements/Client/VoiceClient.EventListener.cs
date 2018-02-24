using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {

        private void AttachToStatusChangeEvents()
        {
            Server.OnClientMicrophoneMuteChanged += OnClientMicrophoneChanged;
            Server.OnClientSpeakersMuteChanged += OnClientSpeakersMuteChanged;
        }

        private void OnClientSpeakersMuteChanged(TClient client, bool isMuted)
        {
            if (!ReferenceEquals(this, client))
            {
                return;
            }

            Speakers = !isMuted;
        }

        private void DetachFromStatusChangeEvents()
        {
            Server.OnClientMicrophoneMuteChanged -= OnClientMicrophoneChanged;
        }

        private void OnClientMicrophoneChanged(TClient client, bool isMuted)
        {
            if (!ReferenceEquals(this, client))
            {
                return;
            }

            Microphone = !isMuted;
        }
        
        
    }
}
