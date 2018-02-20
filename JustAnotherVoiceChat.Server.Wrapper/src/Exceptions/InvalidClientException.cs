using System;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Exceptions
{
    public class InvalidClientException : Exception
    {

        public InvalidClientException() : base("The provided voiceclient doesn't exist")
        {
            
        }

        public InvalidClientException(ushort handle) : base($"The provided client (UID {handle}) doesn't exist")
        {
            
        }

        public InvalidClientException(VoiceHandle handle) : this(handle.Identifer)
        {
            
        }
        
    }
}
