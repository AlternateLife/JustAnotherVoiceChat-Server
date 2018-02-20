using System;

namespace JustAnotherVoiceChat.Server.Wrapper.Interfaces
{
    internal interface IVoiceTaskExecutor : IDisposable
    {

        void Start();
        void Stop();

    }
}
