using System;
using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Elements.Server;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements
{
    internal class VoiceGroup : IVoiceGroup
    {
        public event Delegates.ClientEvent OnClientJoined;
        public event Delegates.ClientEvent OnClientLeft;
        
        private readonly VoiceServer _server;
        private readonly IDictionary<VoiceHandle, IVoiceClient> _clients = new Dictionary<VoiceHandle, IVoiceClient>();

        public IEnumerable<IVoiceClient> Clients
        {
            get
            {
                lock (_clients)
                {
                    return _clients.Values;
                }
            }
        }
        
        internal VoiceGroup(VoiceServer server)
        {
            _server = server;
        }

        public void AddClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            lock (_clients)
            {
                _clients.Add(client.Handle, client);
            }
            
            _server.FireClientJoinedGroup(client, this);
        }

        public void RemoveClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            _server.FireClientLeftGroup(client, this);
            
            lock (_clients)
            {
                _clients.Remove(client.Handle);
            }
        }

        public bool HasClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            lock (_clients)
            {
                return _clients.ContainsKey(client.Handle);
            }
        }

        public void Dispose()
        {
            foreach (var client in Clients)
            {
                _server.FireClientLeftGroup(client, this);
            }

            OnClientJoined = null;
            OnClientLeft = null;
            
            _clients.Clear();
            
            GC.SuppressFinalize(this);
        }
    }
}
