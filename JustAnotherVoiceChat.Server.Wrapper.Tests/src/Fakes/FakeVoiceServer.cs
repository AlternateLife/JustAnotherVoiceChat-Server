using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceServer : VoiceServer<IFakeVoiceClient, byte>
    {
        protected internal FakeVoiceServer(IVoiceClientFactory<IFakeVoiceClient, byte> factory, string hostname, ushort port, int channelId, float globalRollOffScale = 1, float globalDistanceFactor = 1, double globalMaxDistance = 6, IVoiceWrapper<IFakeVoiceClient> voiceWrapper = null) : base(factory, hostname, port, channelId, globalRollOffScale, globalDistanceFactor, globalMaxDistance, voiceWrapper)
        {
            
        }
        
    }
}
