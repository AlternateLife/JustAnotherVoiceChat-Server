using System;

namespace AlternateVoice.Server.Wrapper.Exceptions
{
    public class VoiceServerNotStartedException : Exception
    {

        public VoiceServerNotStartedException() : base("The AlternateVoice Server has not been started yet")
        {
            
        }
        
    }
}
