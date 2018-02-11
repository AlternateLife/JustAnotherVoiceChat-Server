/*
 * File: VoiceWrapper3D.Wrapper.cs
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

using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.Wrapper3D
{
    internal partial class VoiceWrapper3D
    {

#if LINUX
        private const string AlternateVoiceLib = "libAlternateVoice.Server.so";
#else
        private const string AlternateVoiceLib = "AlternateVoice.Server.dll";
#endif

        /**
         * 3D Voice
         */

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_SetClientPosition(ushort clientId, float x, float y, float z, float rotation);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_SetRelativePositionForClient(ushort listenerId, ushort speakerId, float x, float y, float z);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_ResetRelativePositionForClient(ushort listenerId, ushort speakerId);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_ResetAllRelativePositions(ushort clientId);
    }
}
