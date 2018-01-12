using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceServer
    {
        
        void Start();
        void Stop();

        string CreateConnectionString();

        IVoiceGroup CreateGroup();
        IEnumerable<IVoiceGroup> GetAllGroups();
        void DestroyGroup(IVoiceGroup voiceGroup);

        IVoiceClient GetClientByHandle(VoiceHandle handle);

    }
}
