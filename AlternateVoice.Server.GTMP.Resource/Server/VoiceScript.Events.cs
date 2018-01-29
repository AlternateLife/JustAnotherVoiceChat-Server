using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Gta.Tasks;

namespace AlternateVoice.Server.GTMP.Resource
{
    public partial class VoiceScript
    {

        private void AttachServerEvents(IGtmpVoiceServer server)
        {
            server.OnServerStarted += () =>
            {
                API.consoleOutput(LogCat.Info, $"GtmpVoiceServer started, listening on {server.Hostname}:{server.Port}");
            };
            
            server.OnServerStopping += () =>
            {
                API.consoleOutput(LogCat.Info, "GtmpVoiceServer stopping!");
            };

            server.OnClientPrepared += c =>
            {
                c.Player.triggerEvent("VOICE_SET_HANDSHAKE", true, c.HandshakeUrl);
            };
            
            server.OnClientConnected += c =>
            {
                c.Player.triggerEvent("VOICE_SET_HANDSHAKE", false);
            };
            
            server.OnClientDisconnected += c =>
            {
                c.Player.triggerEvent("VOICE_SET_HANDSHAKE", true, c.HandshakeUrl);
            };

            server.OnPlayerStartsTalking += OnPlayerStartsTalking;
            server.OnPlayerStopsTalking += OnPlayerStopsTalking;
        }
        
        private void OnPlayerStartsTalking(IGtmpVoiceClient speakingClient)
        {
            API.playPlayerAnimation(speakingClient.Player, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), "mp_facial", "mic_chatter");
            // Needs further investigation of animation handling 
            //API.sendNativeToPlayersInRange(speakingClient.Player.position, 300f, Hash.TASK_PLAY_ANIM, speakingClient.Player.handle, "mp_facial", "mic_chatter",
            //  8f, -4f, -1, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), 0.0f, false, false, false);
        }

        private void OnPlayerStopsTalking(IGtmpVoiceClient speakingClient)
        {
            // Stopping all animations?
            API.stopPlayerAnimation(speakingClient.Player);
            // Needs further investigation of animation handling 
            // API.sendNativeToPlayersInRange(speakingClient.Player.position, 300f, Hash.TASK_PLAY_ANIM, speakingClient.Player.handle, "mp_facial", "mic_chatter1",
            //    8f, -4f, -1, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), 0.0f, false, false, false);
        }
        
    }
}
