using System;
using System.Collections.Concurrent;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace AlternateVoice.Server.GTMP.Elements
{
    public class VoiceToClientMapper : IVoiceToClientMapper
    {

        private readonly ConcurrentDictionary<NetHandle, VoiceHandle> _mappings = new ConcurrentDictionary<NetHandle, VoiceHandle>();
        
        internal VoiceToClientMapper()
        {
            
        }
        
        public Client GetGtmpClient(IVoiceClient voiceClient)
        {
            throw new NotImplementedException();
        }

        public IVoiceClient GetVoiceClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
