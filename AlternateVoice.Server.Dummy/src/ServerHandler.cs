/*
 * File: ServerHandler.cs
 * Date: 29.1.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
                _logger.Debug($"AlternateVoice: Listening on {_server.Hostname}:{_server.Port}");
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
