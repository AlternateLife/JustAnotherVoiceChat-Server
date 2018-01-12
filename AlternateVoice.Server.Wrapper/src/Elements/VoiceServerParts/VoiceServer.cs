using System;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.VoiceServerParts
{
    public partial class VoiceServer : IVoiceServer
    {
        
        public string Hostname { get; }
        public ushort Port { get; }
        public int ChannelId { get; }

        internal VoiceServer(string hostname, ushort port, int channelId)
        {
            Hostname = hostname;
            Port = port;
            ChannelId = channelId;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public string CreateConnectionString()
        {
            throw new NotImplementedException();
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
