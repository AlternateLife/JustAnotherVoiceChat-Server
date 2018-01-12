namespace AlternateVoice.Server.Structs
{
    public struct VoiceHandle
    {

        private ushort Identifer { get; }

        public VoiceHandle(ushort identifer)
        {
            Identifer = identifer;
        }

        public bool Equals(VoiceHandle other)
        {
            return Identifer == other.Identifer;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is VoiceHandle && Equals((VoiceHandle) obj);
        }

        public override int GetHashCode()
        {
            return Identifer.GetHashCode();
        }

        public static bool operator ==(VoiceHandle left, VoiceHandle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(VoiceHandle left, VoiceHandle right)
        {
            return !left.Equals(right);
        }
    }
}
