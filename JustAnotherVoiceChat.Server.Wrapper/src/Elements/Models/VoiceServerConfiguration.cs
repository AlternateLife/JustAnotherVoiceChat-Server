namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Models
{
    public class VoiceServerConfiguration
    {
        
        public string Hostname { get; }
        public ushort Port { get; }
        
        public string TeamspeakServerId { get; }
        public ulong TeamspeakChannelId { get; }
        public string TeamspeakChannelPassword { get; }
        
        public float GlobalRollOffScale { get; }
        public float GlobalDistanceFactor { get; }
        public double GlobalMaxDistance { get; }

        public VoiceServerConfiguration(string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword)
            : this(hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword, 1, 1, 10f)
        {
        }

        public VoiceServerConfiguration(string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword, float globalRollOffScale, float globalDistanceFactor, double globalMaxDistance)
        {
            Hostname = hostname;
            Port = port;

            TeamspeakServerId = teamspeakServerId;
            TeamspeakChannelId = teamspeakChannelId;
            TeamspeakChannelPassword = teamspeakChannelPassword;

            GlobalRollOffScale = globalRollOffScale;
            GlobalDistanceFactor = globalDistanceFactor;
            GlobalMaxDistance = globalMaxDistance;
        }
        
    }
}
