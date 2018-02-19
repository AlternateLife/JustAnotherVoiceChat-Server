namespace JustAnotherVoiceChat.Server.Wrapper.Delegates
{
    public class NativeDelegates
    {
        public delegate bool ClientConnectCallback(ushort handle);
        public delegate void ClientCallback(ushort handle);
        public delegate void ClientStatusCallback(ushort handle, bool newStatus);
    }
}
