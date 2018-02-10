/*
 * File: VoiceWrapper3D.cs
 * Date: 10.2.2018,
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

using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Wrapper3D
{
    public partial class VoiceWrapper3D : IVoiceWrapper3D
    {

        public void ResetAllRelativePositionsForListener(IVoiceClient listener)
        {
            AV_ResetAllRelativePositions(listener.Handle.Identifer);
        }

        public void ResetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker)
        {
            AV_ResetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer);
        }

        public void SetListenerPosition(IVoiceClient listener)
        {
            AV_SetClientPosition(listener.Handle.Identifer, listener.Position.X, listener.Position.Y, listener.Position.Z, listener.CameraRotation);
        }

        public void SetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker)
        {
            AV_SetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer, speaker.Position.X, speaker.Position.Y, speaker.Position.Z);
        }
    }
}
