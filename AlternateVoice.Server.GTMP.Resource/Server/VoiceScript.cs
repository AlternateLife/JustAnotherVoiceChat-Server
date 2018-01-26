using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.GTMP.Server;
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
            _voiceServer = new GtmpVoiceServer(API, "localhost", 23332, 2);

            _voiceServer.OnServerStarted += () =>
            {
                API.consoleOutput(LogCat.Info, $"GtmpVoiceServer started, listening on {_voiceServer.Hostname}:{_voiceServer.Port}");
            };
            
            _voiceServer.OnServerStopping += () =>
            {
                API.consoleOutput(LogCat.Info, "GtmpVoiceServer stopping!");
            };
        }
        
        [Command("startvoice")]
        public void StartVoiceServer(Client sender, string hostname, ushort port, int channelId)
        {
            if (_voiceServer.Started)
            {
                sender.sendChatMessage("~r~Der VoiceServer wurde bereits gestartet!");
                return;
            }
            
            _voiceServer.Start();
            
            sender.sendChatMessage("Der VoiceServer sollte nun gestartet sein...");
        }

        [Command("stopvoice")]
        public void StopVoiceServer(Client sender)
        {
            if (!_voiceServer.Started)
            {
                sender.sendChatMessage("~r~Der VoiceServer läuft derzeit nicht!");
                return;
            }
            
            _voiceServer.Stop();
            
            sender.sendChatMessage("Der VoiceServer sollte nun stoppen...");
        }
        
    }
}
