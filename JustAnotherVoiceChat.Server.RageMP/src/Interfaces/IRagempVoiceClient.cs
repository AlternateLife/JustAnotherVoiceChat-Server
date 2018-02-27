using GTANetworkAPI;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Interfaces
{
    public interface IRagempVoiceClient : IVoiceClient
    {
        
        Client Player { get; }
        
    }
}
