using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks
{
    public class PositionalVoiceTask<TClient> : IVoiceTask<TClient> where TClient : IVoiceClient
    {
        public virtual int RunVoiceTask(IVoiceServer<TClient> server)
        {
            var native = server.NativeWrapper;
            
            foreach (var client in server.GetClients())
            {
                native.SetListenerPosition(client, client.Position, client.CameraRotation);
                //server.Log(LogLevel.Debug,  $"Set position of {client.Handle.Identifer}: {client.Position} -> {client.CameraRotation}");
            }

            return 50;
        }
        
        public virtual void Dispose()
        {
            
        }
    }
}
