using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks
{
    public class PositionalVoiceTask<TClient> : IVoiceTask<TClient> where TClient : IVoiceClient
    {
        public virtual int RunVoiceTask(IVoiceServer<TClient> server)
        {
            foreach (var client in server.GetClients())
            {
                client.SetListeningPositionToCurrentPosition();
            }

            return 125;
        }
        
        public virtual void Dispose()
        {
            
        }
    }
}
