namespace JustAnotherVoiceChat.Server.Wrapper.Delegates
{
    public class NativeDelegates
    {
        public delegate void ClientCallback(ushort handle);
        public delegate bool ClientConnectingCallback(ushort handle, string teamspeakId);
        public delegate void ClientStatusCallback(ushort handle, bool newStatus);

        public delegate void LogMessageCallback(string message, int loglevel);
    }
}
