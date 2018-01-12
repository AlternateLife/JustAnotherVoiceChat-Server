using System;
using System.Collections.Generic;
using System.Linq;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements
{
    public class VoiceServer : IVoiceServer
    {
        private readonly string _hostname;
        private readonly ushort _port;
        private readonly int _channelId;
        
        private readonly List<VoiceGroup> _groups = new List<VoiceGroup>();

        internal VoiceServer(string hostname, ushort port, int channelId)
        {
            _hostname = hostname;
            _port = port;
            _channelId = channelId;
        }
        
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public string CreateConnectionString()
        {
            throw new System.NotImplementedException();
        }

        public IVoiceGroup CreateGroup()
        {
            var createdGroup = new VoiceGroup();
            lock (_groups)
            {
                _groups.Add(createdGroup);
            }

            return createdGroup;
        }

        public IEnumerable<IVoiceGroup> GetAllGroups()
        {
            lock (_groups)
            {
                return _groups.ToList();
            }
        }

        public void DestroyGroup(IVoiceGroup voiceGroup)
        {
            if (voiceGroup == null)
            {
                throw new ArgumentNullException(nameof(voiceGroup));
            }
            
            voiceGroup.Dispose();
        }

        public IVoiceClient GetClientByHandle(VoiceHandle handle)
        {
            throw new System.NotImplementedException();
        }
    }
}
