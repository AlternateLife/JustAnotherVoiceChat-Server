using System;
using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements
{
    public class VoiceGroup : IVoiceGroup
    {
        
        public IEnumerable<IVoiceClient> Clients { get; }

        internal VoiceGroup()
        {
            
        }

        public void AddClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            
        }

        public void RemoveClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            
        }

        public bool HasClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return false;
        }

        public void Dispose()
        {
            
        }
    }
}
