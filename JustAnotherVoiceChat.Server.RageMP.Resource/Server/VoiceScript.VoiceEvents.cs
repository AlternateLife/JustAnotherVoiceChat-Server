using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.RageMP.Resource.Server.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Enums;

namespace JustAnotherVoiceChat.Server.RageMP.Resource
{
    public partial class VoiceScript
    {
        private void AttachToVoiceServerEvents()
        {
            _voiceServer.OnServerStarted += () =>
            {
                var config = _voiceServer.Configuration;
                NAPI.Util.ConsoleOutput($"JustAnotherVoiceChatServer started, listening on {config.Hostname}:{config.Port}");
            };
            
            _voiceServer.OnServerStopping += () =>
            {
                NAPI.Util.ConsoleOutput("JustAnotherVoiceChatServer stopping!");
            };
            
            _voiceServer.OnLogMessage += OnLogMessage;

            _voiceServer.OnClientPrepared += OnClientPrepared;
            
            _voiceServer.OnClientConnected += OnClientConnected;
            _voiceServer.OnClientRejected += OnClientRejected;
            _voiceServer.OnClientDisconnected += OnHandshakeShouldResend;

            _voiceServer.OnClientTalkingChanged += OnPlayerTalkingChanged;

            _voiceServer.OnClientMicrophoneMuteChanged += (client, newStatus) =>
            {
                NAPI.Util.ConsoleOutput($"{client.Player.Name} {(newStatus ? "muted" : "unmuted")} the microphone!");
                client.Player.SendChatMessage($"You {(newStatus ? "~r~muted" : "~g~unmuted")}~s~ your microphone!");
            };
            
            _voiceServer.OnClientSpeakersMuteChanged += (client, newStatus) =>
            {
                NAPI.Util.ConsoleOutput($"{client.Player.Name} {(newStatus ? "muted" : "unmuted")} the speakers!");
                client.Player.SendChatMessage($"You {(newStatus ? "~r~muted" : "~g~unmuted")}~s~ your speakers!");
            };
        }

        private void OnLogMessage(string message, LogLevel logLevel)
        {
            NAPI.Util.ConsoleOutput(message);
        }

        private void OnClientConnected(IRagempVoiceClient client)
        {
            client.Player.TriggerEvent("voiceSetHandhsake", false);
            client.SetNickname(client.Player.Name);
        }

        private void OnClientRejected(IRagempVoiceClient client, StatusCode statusCode)
        {
            NAPI.Util.ConsoleOutput($"The voice-connection of {client.Player.Name} has been rejected: {statusCode.ToString()}");
            client.Player.Kick($"~r~Your voice-connection has been rejected: {statusCode.ToString()}");
        }

        private void OnClientPrepared(IRagempVoiceClient client)
        {
            _voiceServer.NativeWrapper.SetClientVoiceRange(client, 15);
            
            OnHandshakeShouldResend(client);
        }

        private void OnHandshakeShouldResend(IRagempVoiceClient client)
        {
            client.Player.TriggerEvent("voiceSetHandhsake", true, client.HandshakeUrl);
        }
        
        private void OnPlayerTalkingChanged(IRagempVoiceClient speakingClient, bool newStatus)
        {
            if (newStatus)
            {
                NAPI.Player.PlayPlayerAnimation(speakingClient.Player, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), "mp_facial", "mic_chatter");
            }
            else
            {
                NAPI.Player.StopPlayerAnimation(speakingClient.Player);
            }
        }
    }
}
