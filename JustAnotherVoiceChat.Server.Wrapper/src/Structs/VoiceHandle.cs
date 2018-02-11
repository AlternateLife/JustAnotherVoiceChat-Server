/*
 * File: VoiceHandle.cs
 * Date: 29.1.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace JustAnotherVoiceChat.Server.Wrapper.Structs
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
