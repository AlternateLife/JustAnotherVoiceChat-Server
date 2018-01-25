using System;
using System.Collections.Concurrent;
using System.Threading;
using AlternateVoice.Server.Wrapper.Interfaces;
using NLog;

namespace AlternateVoice.Server.Dummy
{
    public class ServerHandler : IDisposable
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IVoiceServer _server;
        
        private readonly ConcurrentBag<IVoiceClient> _voiceClients = new ConcurrentBag<IVoiceClient>();
        

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

        public IVoiceClient PrepareClient()
        {
            return _server.CreateClient();
        }

        public void Dispose()
        {
            _server.Dispose();
        }

        public void StartStresstest()
        {
            for (var i = 0; i < 20; i++)
            {
                new Thread(ClientPrepareThread).Start();
            }
            for (var i = 0; i < 20; i++)
            {
                new Thread(ClientRemoveThread).Start();
            }
        }
        
        public void ClientPrepareThread()
        {
            while (true)
            {
                var createdClient = _server.CreateClient();

                if (createdClient == null)
                {
                    _logger.Warn("Failed to create client!");
                }
                else
                {
                    _logger.Info("Created client: " + createdClient.Handle.Identifer);
                }

                _voiceClients.Add(createdClient);
                
                Thread.Sleep(1);
            }
        }

        public void ClientRemoveThread()
        {
            while (true)
            {
                Thread.Sleep(2);
                
                IVoiceClient client;

                if (!_voiceClients.TryTake(out client))
                {
                    continue;
                }

                if (_server.RemoveClient(client))
                {
                    _logger.Info("Removed client: " + client.Handle.Identifer);
                }
                else
                {
                    _logger.Warn("Failed to remove client: " + client.Handle.Identifer);
                }
            }
        }
        
    }
}
