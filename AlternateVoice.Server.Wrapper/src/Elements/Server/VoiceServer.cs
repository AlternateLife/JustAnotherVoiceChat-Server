using System;
using AlternateVoice.Server.Wrapper.Exceptions;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer : IVoiceServer
    {
        
        public string Hostname { get; }
        public ushort Port { get; }
        public int ChannelId { get; }
        public bool Started { get; private set; }
        
        internal VoiceServer(string hostname, ushort port, int channelId)
        {
            var result = Uri.CheckHostName(hostname);

            if (result == UriHostNameType.Unknown)
            {
                throw new InvalidHostnameException(hostname);
            }
            
            Hostname = hostname;
            Port = port;
            ChannelId = channelId;
        }

        public void Start()
        {
            if (Started)
            {
                throw new VoiceServerAlreadyStartedException();
            }
            
            AL_StartServer(Hostname, Port, ChannelId);
            
            Started = true;
            OnServerStarted?.Invoke();
        }

        public void Stop()
        {
            if (!Started)
            {
                throw new VoiceServerNotStartedException();
            }
            
            AL_StopServer();
            
            OnServerStopping?.Invoke();
            Started = false;
        }

        public void Dispose()
        {
            DisposeGroups();
            
            DisposeEvents();
        }
    }
}
