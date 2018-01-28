using AlternateVoice.Server.GTMP.Factories;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;

namespace AlternateVoice.Server.GTMP.Resource
{
    public class VoiceScript : Script
    {

        private readonly IGtmpVoiceServer _voiceServer;

        public VoiceScript()
        {
            _voiceServer = GtmpVoice.CreateServer(API, "game.alternate-life.de", 23332, 23);

            _voiceServer.OnServerStarted += () =>
            {
                API.consoleOutput(LogCat.Info, $"GtmpVoiceServer started, listening on {_voiceServer.Hostname}:{_voiceServer.Port}");
            };
            
            _voiceServer.OnServerStopping += () =>
            {
                API.consoleOutput(LogCat.Info, "GtmpVoiceServer stopping!");
            };

            _voiceServer.OnClientPrepared += c =>
            {
                var player = c.Player;
                player.sendChatMessage("HANDSHAKE: " + c.HandshakeUrl);
                player.triggerEvent("VOICE_SET_HANDSHAKE", true, c.HandshakeUrl);
            };

            API.onClientEventTrigger += (sender, name, arguments) =>
            {
                if (name != "VOICE_ROTATION")
                {
                    return;
                }

                var rotation = (float)arguments[0];
                var voiceClient = _voiceServer.GetVoiceClientOfPlayer(sender);

                if (voiceClient == null)
                {
                    return;
                }

                voiceClient.CameraRotation = rotation;
            };

            _voiceServer.Start();
            
            API.onResourceStop += OnResourceStop;
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }

        [Command("handshake")]
        public void VoicePlayerConnect(Client sender, bool status)
        {
            var voiceClient = _voiceServer.GetVoiceClientOfPlayer(sender);
            
            sender.triggerEvent("VOICE_SET_HANDSHAKE", status, voiceClient.HandshakeUrl);
        }
    }
}
