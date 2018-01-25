using System;

namespace AlternateVoice.Server.Wrapper.Exceptions
{
    public class VoiceServerAlreadyStartedException : Exception
    {

        public VoiceServerAlreadyStartedException() : base("The AlternateVoice Server was already started!")
        {
            
        }
        
    }
}
