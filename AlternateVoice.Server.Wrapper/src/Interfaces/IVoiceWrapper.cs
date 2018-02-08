namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceWrapper
    {
        void StartNativeServer(ushort port);
        void StopNativeServer();

        void RemoveClient(IVoiceClient client);

        void SetClientPositionForClient(IVoiceClient listener, IVoiceClient client);
        void MuteClientForClient(IVoiceClient listener, IVoiceClient client, bool muted);
        void SetListenerDirection(IVoiceClient listener);

        void TestCallClientTalkingChangedCallback(ushort handle, bool newStatus);
        void TestCallClientConnectedCallback(ushort handle);
        void TestCallClientDisconnectedCallback(ushort handle);

        void UnregisterClientConnectedCallback();
        void UnregisterClientDisconnectedCallback();

        void RegisterClientConnectedCallback(Delegates.ClientConnectCallback callback);
        void RegisterClientDisconnectedCallback(Delegates.ClientCallback callback);
        void RegisterClientTalkingChangedCallback(Delegates.ClientStatusCallback callback);
    }
}
