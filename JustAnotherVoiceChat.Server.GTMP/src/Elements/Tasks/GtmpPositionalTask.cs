using System.Linq;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Elements.Tasks
{
    public class GtmpPositionalTask : IVoiceTask<IGtmpVoiceClient>
    {
        private readonly int _sleepTime;

        public GtmpPositionalTask(int sleepTime = 125)
        {
            _sleepTime = sleepTime;
        }
        
        public virtual int RunVoiceTask(IVoiceServer<IGtmpVoiceClient> server)
        {
            var clientPositions = server.GetClients().Select(client =>
            {
                if (client.Player.vehicle == null)
                {
                    return client.MakeClientPosition();
                }
                else
                {
                    return client.MakeClientPosition(client.Player.vehicle.position, client.CameraRotation);
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
