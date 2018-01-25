using System;
using AlternateVoice.Server.Wrapper.Exceptions;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.VoiceServerParts
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
            
            Started = true;
            OnServerStarted?.Invoke();
        }

        public void Stop()
        {
            if (!Started)
            {
                throw new VoiceServerNotStartedException();
            }
            
            OnServerStopping?.Invoke();
            Started = false;
        }

        public string CreateConnectionString()
        {
            return Uri.EscapeUriString($"{Hostname}/{Port}");
        }

        public IVoiceClient GetClientByHandle(VoiceHandle handle)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            DisposeGroups();
            
            DisposeEvents();
        }
    }
}
