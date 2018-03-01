/*
 * File: VoiceClient.Native.Positional.cs
 * Date: 21.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
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

using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {
        public bool SetListeningPosition(Vector3 position, float rotation)
        {
            return RunWhenConnected(() => Server.NativeWrapper.SetListenerPosition(this, position, rotation));
        }

        public ClientPosition MakeClientPosition(Vector3 position, float rotation)
        {
            return new ClientPosition(Handle.Identifer, position.X, position.Y, position.Z, rotation);
        }

        public ClientPosition MakeClientPosition()
        {
            var position = Position;
            return new ClientPosition(Handle.Identifer, position.X, position.Y, position.Z, CameraRotation);
        }

        public bool SetRelativeSpeakerPosition(IVoiceClient speaker, Vector3 position)
        {
            return RunWhenConnected(() => Server.NativeWrapper.SetRelativeSpeakerPositionForListener(this, speaker, position));
        }
        
        public bool ResetRelativeSpeakerPosition(IVoiceClient speaker)
        {
            return RunWhenConnected(() => Server.NativeWrapper.ResetRelativeSpeakerPositionForListener(this, speaker));
        }

        public bool ResetAllRelativeSpeakerPositions()
        {
            return RunWhenConnected(() => Server.NativeWrapper.ResetAllRelativePositionsForListener(this));
        }
        
        public bool SetVoiceRange(float range)
        {
            return RunWhenConnected(() => Server.NativeWrapper.SetClientVoiceRange(this, range));
        }
    }
}
