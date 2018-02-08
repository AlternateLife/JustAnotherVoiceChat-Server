using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        private readonly List<GCHandle> _garbageCollectorHandles = new List<GCHandle>();

        public void RegisterEvent<T>(Action<T> register, T callback)
        {
            _garbageCollectorHandles.Add(GCHandle.Alloc(callback));

            register(callback);
        }

        private void AttachToNativeEvents()
        {
            RegisterEvent<Delegates.ClientConnectCallback>(_voiceWrapper.RegisterClientConnectedCallback, OnClientConnectedFromVoice);
            RegisterEvent<Delegates.ClientCallback>(_voiceWrapper.RegisterClientDisconnectedCallback, OnClientDisconnectedFromVoice);
            RegisterEvent<Delegates.ClientStatusCallback>(_voiceWrapper.RegisterClientTalkingChangedCallback, OnClientTalkingStatusChangeFromVoice);
        }

        private void DisposeNativeEvents()
        {
            _voiceWrapper.UnregisterClientConnectedCallback();
            _voiceWrapper.UnregisterClientDisconnectedCallback();

            foreach (var handle in _garbageCollectorHandles)
            {
                handle.Free();
            }
        }
    }
}
