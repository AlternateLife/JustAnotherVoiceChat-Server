using System;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Clients
{
    internal class GtmpVoiceClient : IGtmpVoiceClient
    {
        public IVoiceClient VoiceClient { get; }
        public Client Player { get; }
        
        public bool Headphones => VoiceClient.Headphones;
        public bool Microphone => VoiceClient.Microphone;
        public string HandshakeUrl => VoiceClient.HandshakeUrl;

        internal GtmpVoiceClient(Client player, IVoiceClient client)
        {
            Player = player;
            VoiceClient = client;
        }

        public void Dispose()
        {
            VoiceClient.Dispose();
            
            GC.SuppressFinalize(this);
        }
        
    }
}
