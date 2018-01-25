using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.API;

namespace AlternateVoice.Server.GTMP
{
    public class GtmpVoiceServer : IGtmpVoiceServer
    {

        private readonly IVoiceServer _server;
        private readonly API _api;

        public GtmpVoiceServer(API api, string hostname, ushort port, int channelId)
        {
            _api = api;

            _server = Wrapper.AlternateVoice.MakeServer(hostname, port, channelId);
        }
        
        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }
}
