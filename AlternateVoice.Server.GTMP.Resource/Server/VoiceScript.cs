using System;
using AlternateVoice.Server.GTMP.Factories;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;

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
                c.Player.sendChatMessage("HANDSHAKE: " + c.HandshakeUrl);
            };
            
            _voiceServer.Start();
            
            API.onResourceStop += OnResourceStop;
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }
    }
}
