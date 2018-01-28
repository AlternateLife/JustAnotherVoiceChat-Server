using System;
using System.Collections.Concurrent;
using System.Linq;
using AlternateVoice.Server.Wrapper.Elements.Client;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        private readonly ConcurrentDictionary<ushort, VoiceClient> _clients = new ConcurrentDictionary<ushort, VoiceClient>();
        
        private readonly object _voiceHandleGenerationLock = new object();
        
        public IVoiceClient CreateClient()
        {
            lock (_voiceHandleGenerationLock)
            {
                VoiceHandle handle;
                try
                {
                    handle = CreateFreeVoiceHandle();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                
                var createdClient = new VoiceClient(this, handle);

                if (!_clients.TryAdd(handle.Identifer, createdClient))
                {
                    return null;
                }

                return createdClient;
            }
        }

        public bool RemoveClient(IVoiceClient client)
        {
            lock (_voiceHandleGenerationLock)
            {
                VoiceClient removedClient;
                return _clients.TryRemove(client.Handle.Identifer, out removedClient);
            }
        }

        public IVoiceClient GetClientByHandle(VoiceHandle handle)
        {
            return GetClientByHandle(handle.Identifer);
        }

        public IVoiceClient GetClientByHandle(ushort handle)
        {
            lock (_voiceHandleGenerationLock)
            {
                VoiceClient result;
                if (_clients.TryGetValue(handle, out result))
                {
                    return result;
                }
                return null;
            }
        }

        private VoiceHandle CreateFreeVoiceHandle()
        {
            var freeHandle = Enumerable
                .Range(ushort.MinValue + 1, ushort.MaxValue)
                .Select(v => (ushort) v)
                .Except(_clients.Keys.ToArray())
                .First();
            
            return new VoiceHandle(freeHandle);
        }
    }
}
