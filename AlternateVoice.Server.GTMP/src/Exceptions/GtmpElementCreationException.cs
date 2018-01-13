using System;

namespace AlternateVoice.Server.GTMP.Exceptions
{
    public class GtmpElementCreationException : Exception
    {

        public GtmpElementCreationException(Type type) : base($"Failed to store \"{type}\" into FactoryCollection")
        {
            
        }
        
    }
}
