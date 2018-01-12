using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceClient
    {
        
        VoiceHandle Handle { get; }
        
        bool Microphone { get; set; }
        bool Headphones { get; set; }
        
        IEnumerable<IVoiceGroup> Groups { get; }

        void JoinGroup(IVoiceGroup group);
        void LeaveGroup(IVoiceGroup group);

    }
}
