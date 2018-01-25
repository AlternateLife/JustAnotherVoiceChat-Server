using System;
using AlternateVoice.Server.Wrapper.Interfaces;
using NLog;

namespace AlternateVoice.Server.Dummy
{
    public class ServerHandler : IDisposable
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IVoiceServer _server;
        

        public ServerHandler(string hostname, ushort port, int channelId)
        {
            _server = Wrapper.AlternateVoice.MakeServer(hostname, port, channelId);

            _server.OnServerStarted += () =>
            {
                _logger.Debug($"AlternateVoice: Listening to {_server.Hostname}:{_server.Port}");
            };
            
            _server.OnServerStopping += () =>
            {
                _logger.Debug("AlternateVoice: Stopping...");
            };
        }

        public void StartServer()
        {
            _server.Start();
        }

        public void StopServer()
        {
            _server.Stop();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
        
    }
}
