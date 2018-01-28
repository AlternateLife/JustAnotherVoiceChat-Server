using System;
using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceClient : IDisposable
    {
        
        VoiceHandle Handle { get; }
        
        bool Connected { get; }
        
        bool Microphone { get; }
        bool Headphones { get; }
        
        float CameraRotation { get; set; }

        string HandshakeUrl { get; }
        
        IEnumerable<IVoiceGroup> Groups { get; }

        void JoinGroup(IVoiceGroup group);
        void LeaveGroup(IVoiceGroup group);

    }
}
