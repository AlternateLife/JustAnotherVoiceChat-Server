using System.Linq;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Tasks
{
    public class RagempPositionalTask : IVoiceTask<IRagempVoiceClient>
    {
        private readonly int _sleepTime;

        public RagempPositionalTask(int sleepTime = 125)
        {
            _sleepTime = sleepTime;
        }
        
        public virtual int RunVoiceTask(IVoiceServer<IRagempVoiceClient> server)
        {
            var clientPositions = server.GetClients().Select(client =>
            {
                if (client.Player.Vehicle == null)
                {
                    return client.MakeClientPosition();
                }
                else
                {
                    return client.MakeClientPosition(client.Player.Vehicle.Position, client.CameraRotation);
                }
            });

            server.SetPlayerPositions(clientPositions);

            return _sleepTime;
        }
        
        public virtual void Dispose()
        {
            
        }
    }
}
