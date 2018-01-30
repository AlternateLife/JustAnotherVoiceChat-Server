using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        
        private readonly List<GCHandle> _garbageCollectorHandles = new List<GCHandle>();

        private void RegisterEvent<T>(Action<T> register, T callback)
        {
            _garbageCollectorHandles.Add(GCHandle.Alloc(callback));
            
            register(callback);
        }
        
        private void AttachToNativeEvents()
        {
            RegisterEvent<ClientCallback>(AV_RegisterNewClientCallback, OnClientConnectedFromVoice);
        }

        private void DisposeNativeEvents()
        {
            AV_UnregisterNewClientCallback();

            foreach (var handle in _garbageCollectorHandles)
            {
                handle.Free();
            }
        }
        
    }
}
