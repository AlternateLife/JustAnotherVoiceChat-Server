using System;
using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceClient : IDisposable
    {
        
        VoiceHandle Handle { get; }
        
        bool Microphone { get; }
        bool Headphones { get; }
        
        string HandshakeUrl { get; }
        
        IEnumerable<IVoiceGroup> Groups { get; }

        void JoinGroup(IVoiceGroup group);
        void LeaveGroup(IVoiceGroup group);

    }
}
