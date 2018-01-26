using System;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer : IGtmpVoiceServer
    {

        private readonly IVoiceServer _server;
        private readonly API _api;

        public string Hostname => _server.Hostname;
        public ushort Port => _server.Port;
        public int ChannelId => _server.ChannelId;
        
        public bool Started => _server.Started;
        
        public GtmpVoiceServer(API api, string hostname, ushort port, int channelId)
        {
            _api = api;

            _server = Wrapper.AlternateVoice.MakeServer(hostname, port, channelId);
            
            AttachToEvents();
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public void Dispose()
        {
            
            GC.SuppressFinalize(this);
        }
    }
}
