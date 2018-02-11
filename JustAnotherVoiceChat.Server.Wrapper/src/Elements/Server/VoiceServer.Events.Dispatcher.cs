using System;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Client;
using JustAnotherVoiceChat.Server.Wrapper.Enums;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        
        private bool OnClientConnectedFromVoice(ushort handle)
        {
            return RunWhenClientValid(handle, client =>
            {
                if (client.Connected)
                {
                    return false;
                }
                
                client.Connected = true;
                OnClientConnected?.Invoke(client);

                return true;
            });
        }

        private void OnClientDisconnectedFromVoice(ushort handle)
        {
            RunWhenClientConnected(handle, client =>
            {
                client.Connected = false;
                
                OnClientDisconnected?.Invoke(client, DisconnectReason.Quit);
            });
        }

        private void OnClientTalkingStatusChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientTalkingChanged?.Invoke(client, newStatus);
            });
        }

        private void OnClientSpeakersMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientSpeakersMuteChanged?.Invoke(client, newStatus);
            });
        }

        private void OnClientMicrophoneMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientMicrophoneMuteChanged?.Invoke(client, newStatus);
            });
        }
        
        
        
        private T RunWhenClientValid<T>(ushort handle, Func<VoiceClient, T> callback)
        {
            var client = GetClientByHandle(handle) as VoiceClient;

            if (client == null)
            {
                return default(T);
            }

            return callback(client);
        }

        private void RunWhenClientValid(ushort handle, Action<VoiceClient> callback)
        {
            var client = GetClientByHandle(handle) as VoiceClient;

            if (client == null)
            {
                return;
            }
            
            callback(client);
        }
        
        private T RunWhenClientConnected<T>(ushort handle, Func<VoiceClient, T> callback)
        {
            return RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return default(T);
                }

                return callback(client);
            });
        }

        private void RunWhenClientConnected(ushort handle, Action<VoiceClient> callback)
        {
            RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return;
                }

                callback(client);
            });
        }
    }
}
