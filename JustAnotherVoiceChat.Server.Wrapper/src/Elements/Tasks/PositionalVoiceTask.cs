using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks
{
    public class PositionalVoiceTask<TClient> : IVoiceTask<TClient> where TClient : IVoiceClient
    {
        public virtual async Task RunVoiceTask(IVoiceServer<TClient> server)
        {
            var native = server.NativeWrapper;

            foreach (var client in server.GetClients())
            {
                native.SetListenerPosition(client, client.Position, client.CameraRotation);
                //server.Log(LogLevel.Debug,  $"Set position of {client.Handle.Identifer}: {client.Position} -> {client.CameraRotation}");
            }
            
            await Task.Delay(50).ConfigureAwait(false);
        }
        
        public virtual void Dispose()
        {
            
        }
    }
}
