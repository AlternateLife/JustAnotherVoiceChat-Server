using System;
using System.Collections.Generic;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceGroup : IDisposable
    {
        
        IEnumerable<IVoiceClient> Clients { get; }

        void AddClient(IVoiceClient client);
        void RemoveClient(IVoiceClient client);
        bool HasClient(IVoiceClient client);

    }
}
