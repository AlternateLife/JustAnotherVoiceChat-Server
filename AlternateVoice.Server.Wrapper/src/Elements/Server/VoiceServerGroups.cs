using System;
using System.Collections.Generic;
using System.Linq;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        
        private readonly List<VoiceGroup> _groups = new List<VoiceGroup>();
        
        public IVoiceGroup CreateGroup()
        {
            var createdGroup = new VoiceGroup(this);
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

        private void DisposeGroups()
        {
            
        }
        
    }
}
