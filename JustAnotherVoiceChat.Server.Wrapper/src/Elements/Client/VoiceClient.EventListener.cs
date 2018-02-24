using System;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {

        private void AttachToStatusChangeEvents()
        {
            Server.OnClientConnected += OnClientConnected;
            Server.OnClientDisconnected += OnClientDisconnected;
            Server.OnClientMicrophoneMuteChanged += OnClientMicrophoneChanged;
            Server.OnClientSpeakersMuteChanged += OnClientSpeakersMuteChanged;
        }

        private void DetachFromStatusChangeEvents()
        {
            Server.OnClientConnected -= OnClientConnected;
            Server.OnClientDisconnected -= OnClientDisconnected;
            Server.OnClientMicrophoneMuteChanged -= OnClientMicrophoneChanged;
            Server.OnClientSpeakersMuteChanged -= OnClientSpeakersMuteChanged;
        }

        private void OnClientConnected(TClient client)
        {
            ExecuteOnMe(client, () => { Connected = true; });
        }

        private void OnClientDisconnected(TClient client)
        {
            ExecuteOnMe(client, () => { Connected = false; });
        }

        private void OnClientSpeakersMuteChanged(TClient client, bool isMuted)
        {
            ExecuteOnMe(client, () => { Speakers = !isMuted; });
        }

        private void OnClientMicrophoneChanged(TClient client, bool isMuted)
        {
            ExecuteOnMe(client, () => { Microphone = !isMuted; });
        }

        private void ExecuteOnMe(TClient client, Action callback)
        {
            if (!ReferenceEquals(this, client))
            {
                return;
            }

            callback();
        }
        
        
    }
}
