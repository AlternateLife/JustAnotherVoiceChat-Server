using System;
using System.Collections.Generic;

namespace AlternateVoice.Server.Interfaces
{
    public interface IVoiceGroup : IDisposable
    {
        
        IEnumerable<IVoiceClient> Clients { get; }

        void AddClient(IVoiceClient client);
        void RemoveClient(IVoiceClient client);
        bool HasClient(IVoiceClient client);

    }
}
