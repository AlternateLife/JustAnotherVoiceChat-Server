using System;

namespace AlternateVoice.Server.Wrapper.Exceptions
{
    public class InvalidHostnameException : Exception
    {

        public InvalidHostnameException(string hostname) : base($"The provided hostname \"{hostname}\" is invalid")
        {
            
        }
        
    }
}
