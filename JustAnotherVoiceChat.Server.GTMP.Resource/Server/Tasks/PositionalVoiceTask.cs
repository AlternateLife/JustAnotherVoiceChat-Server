using System;
using System.Linq;
using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Resource.Tasks
{
    public class PositionalVoiceTask<TClient> : IVoiceTask<TClient> where TClient : IVoiceClient<TClient>
    {

        public async Task RunVoiceTask(IVoiceServer<TClient> server)
        {
            var native = server.NativeWrapper;

            foreach (var client in server.GetClients().ToList())
            {
                native.SetListenerPosition(client, client.Position, client.CameraRotation);
                
                Console.WriteLine("SET POSITION: " + client.Handle.Identifer + " -> " + client.Position + " -> " + client.CameraRotation);
            }
            
            await Task.Delay(333).ConfigureAwait(false);
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
