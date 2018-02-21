using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    internal class FakeVoiceServer : VoiceServer<IFakeVoiceClient, byte>
    {
        public FakeVoiceServer(IVoiceClientFactory<IFakeVoiceClient, byte> factory, VoiceServerConfiguration configuration, IVoiceWrapper voiceWrapper = null) : base(factory, configuration, voiceWrapper)
        {
            
        }
        
    }
}
