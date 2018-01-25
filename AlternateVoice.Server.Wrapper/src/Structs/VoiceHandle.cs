namespace AlternateVoice.Server.Wrapper.Structs
{
    public struct VoiceHandle
    {
        
        public bool IsEmpty => Identifer == 0;
        
        public ushort Identifer { get; }

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

        public static VoiceHandle Empty()
        {
            return new VoiceHandle(0);
        }
    }
}
