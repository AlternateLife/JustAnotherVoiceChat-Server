/*
 * File: VoiceServer.Groups.cs
 * Date: 15.2.2018,
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

using System;
using System.Collections.Generic;
using System.Linq;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Group;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer
    {
        
        private readonly List<VoiceGroup> _groups = new List<VoiceGroup>();
        
        public IVoiceGroup CreateGroup()
        {
            var createdGroup = new VoiceGroup(this);
            lock (_groups)
            {
                _groups.Add(createdGroup);
            }

            return createdGroup;
        }

        public IEnumerable<IVoiceGroup> GetAllGroups()
        {
            lock (_groups)
            {
                return _groups.ToList();
            }
        }

        public void DestroyGroup(IVoiceGroup voiceGroup)
        {
            if (voiceGroup == null)
            {
                throw new ArgumentNullException(nameof(voiceGroup));
            }
            
            voiceGroup.Dispose();
        }

        private void DisposeGroups()
        {
            
        }
        
    }
}
