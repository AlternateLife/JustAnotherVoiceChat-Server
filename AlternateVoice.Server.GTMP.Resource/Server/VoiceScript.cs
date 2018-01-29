using AlternateVoice.Server.GTMP.Factories;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Resource
{
    public partial class VoiceScript : Script
    {

        private readonly IGtmpVoiceServer _voiceServer;

        public VoiceScript()
        {
            _voiceServer = GtmpVoice.CreateServer(API, "game.alternate-life.de", 23332, 23);

            AttachServerEvents(_voiceServer);

            API.onClientEventTrigger += OnClientEventTrigger;
            API.onResourceStop += OnResourceStop;

            _voiceServer.Start();
        }

        private void OnClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            if (eventName != "VOICE_ROTATION")
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
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }
    }
}
