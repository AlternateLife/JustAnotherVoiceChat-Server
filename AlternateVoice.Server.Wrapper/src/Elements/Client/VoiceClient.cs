using System;
using System.Collections.Generic;
using System.Linq;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.Client
{
    internal class VoiceClient : IVoiceClient
    {
        private readonly IVoiceServer _server;

        public VoiceHandle Handle { get; }
        
        public bool Microphone { get; set; }
        public bool Headphones { get; set; }

        public float CameraRotation { get; private set; }
        
        public string HandshakeUrl { get; }

        public IEnumerable<IVoiceGroup> Groups
        {
            get
            {
                return _server.GetAllGroups().Where(c => c.HasClient(this));
            }
        }

        internal VoiceClient(IVoiceServer server, VoiceHandle handle)
        {
            _server = server;

            Handle = handle;
            
            var handshakePayload = Uri.EscapeUriString($"{_server.Hostname}:{_server.Port}:{Handle.Identifer}");
            HandshakeUrl = $"http://localhost:23333/handshake/{handshakePayload}";
        }

        public void JoinGroup(IVoiceGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            
            group.AddClient(this);
        }

        public void LeaveGroup(IVoiceGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            
            group.RemoveClient(this);
        }

        public void SetCameraRotation(float cameraRotation)
        {
            CameraRotation = cameraRotation;
        }

        public void Dispose()
        {
            _server.RemoveClient(this);
        }
    }
}
