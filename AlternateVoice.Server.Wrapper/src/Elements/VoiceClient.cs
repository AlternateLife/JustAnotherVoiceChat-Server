using System;
using System.Collections.Generic;
using System.Linq;
using AlternateVoice.Server.Interfaces;
using AlternateVoice.Server.Structs;

namespace AlternateVoice.Server.Elements
{
    public class VoiceClient : IVoiceClient
    {
        private readonly IVoiceServer _server;

        public VoiceHandle Handle { get; }
        
        public bool Microphone { get; set; }
        public bool Headphones { get; set; }

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
    }
}
