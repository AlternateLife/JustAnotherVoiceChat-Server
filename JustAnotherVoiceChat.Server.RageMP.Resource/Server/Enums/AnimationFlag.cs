using System;

namespace JustAnotherVoiceChat.Server.RageMP.Resource.Server.Enums
{
    [Flags]
    public enum AnimationFlag
    {
        None = 0,
        Loop = 1,
        StayInEndFrame = 2,
        UpperBodyOnly = 0x10,
        AllowRotation = 0x20,
        CancelableWithMovement = 0x80,
        RagdollOnCollision = 0x400000
    }
}
