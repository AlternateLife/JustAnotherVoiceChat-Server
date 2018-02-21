using System;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Models
{
    public class ClientConnectingEventArgs : EventArgs
    {
        
        public bool Reject { get; set; }
        
    }
}
