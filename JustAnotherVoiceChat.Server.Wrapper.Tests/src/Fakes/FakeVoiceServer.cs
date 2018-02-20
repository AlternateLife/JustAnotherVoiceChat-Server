using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class FakeVoiceServer : VoiceServer<IFakeVoiceClient, byte>
    {
        internal FakeVoiceServer(IVoiceClientFactory<IFakeVoiceClient, byte> factory, string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword, IVoiceWrapper<IFakeVoiceClient> voiceWrapper = null) : base(factory, hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword, voiceWrapper)
        {
            
        }
        
        internal FakeVoiceServer(IVoiceClientFactory<IFakeVoiceClient, byte> factory, string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword, float globalRollOffScale = 1, float globalDistanceFactor = 1, double globalMaxDistance = 6, IVoiceWrapper<IFakeVoiceClient> voiceWrapper = null) : base(factory, hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword, globalRollOffScale, globalDistanceFactor, globalMaxDistance, voiceWrapper)
        {
            
        }
        
    }
}
