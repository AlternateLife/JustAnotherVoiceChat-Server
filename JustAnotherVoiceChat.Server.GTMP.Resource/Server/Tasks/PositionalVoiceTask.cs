using System;
using System.Linq;
using System.Threading.Tasks;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Resource.Tasks
{
    public class PositionalVoiceTask<TClient> : IVoiceTask<TClient> where TClient : IVoiceClient<TClient>
    {
        private readonly API _api;

        public PositionalVoiceTask(API api)
        {
            _api = api;
        }

        public async Task RunVoiceTask(IVoiceServer<TClient> server)
        {
            var native = server.NativeWrapper;

            foreach (var client in server.GetClients().ToList())
            {
                native.SetListenerPosition(client, client.Position, client.CameraRotation);
                
                _api.consoleOutput(LogCat.Debug, "SET POSITION: " + client.Handle.Identifer + " -> " + client.Position + " -> " + client.CameraRotation);
            }
            
            await Task.Delay(50).ConfigureAwait(false);
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
