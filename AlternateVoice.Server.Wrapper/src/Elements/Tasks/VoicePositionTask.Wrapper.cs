/*
 * File: VoicePositionTask.Wrapper.cs
 * Date: 2.2.2018,
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

using System.Collections.Concurrent;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Math;

namespace AlternateVoice.Server.Wrapper.Elements.Tasks
{
    internal partial class VoicePositionTask
    {
        private readonly ConcurrentDictionary<ushort, Vector3> _lastPosition = new ConcurrentDictionary<ushort, Vector3>();
        private readonly ConcurrentDictionary<ushort, float> _lastCameraRotation = new ConcurrentDictionary<ushort, float>();

        public bool TryMuteForeignClientForListener(IVoiceClient listenerClient, IVoiceClient foreignClient)
        {
            var distance = listenerClient.Position.Distance(foreignClient.Position);
            var mute = false;

            if (distance >= _voiceServer.GlobalMaxDistance)
            {
                mute = true;
            }

            _voiceServer.MuteClientForListener(listenerClient.Handle.Identifer, foreignClient.Handle.Identifer, mute);
            return mute;
        }

        public bool TrySetForeignClientPositonForListener(IVoiceClient listenerClient, IVoiceClient foreignClient)
        {
            var foreignPosition = foreignClient.Position;
            var listenerId = listenerClient.Handle.Identifer;

            Vector3 lastForeignPosition;
            if (_lastPosition.TryGetValue(foreignClient.Handle.Identifer, out lastForeignPosition))
            {
                if (lastForeignPosition == foreignPosition)
                {
                    return false;
                }
            }

            if (_lastPosition.TryAdd(listenerId, foreignPosition))
            {
                _voiceServer.SetClientPositionForListener(listenerId, foreignClient);
                return true;
            }

            return false;
        }

        public bool TrySetListenerDirection(IVoiceClient listenerClient)
        {
            float lastDirection;
            if (_lastCameraRotation.TryGetValue(listenerClient.Handle.Identifer, out lastDirection))
            {
                if (lastDirection == listenerClient.CameraRotation)
                {
                    return false;
                }
            }

            if (_lastCameraRotation.TryAdd(listenerClient.Handle.Identifer, listenerClient.CameraRotation))
            {
                _voiceServer.SetListenerDirection(listenerClient);
                return true;
            }
            return false;
        }
    }
}
