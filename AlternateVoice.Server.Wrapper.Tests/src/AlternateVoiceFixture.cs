/*
 * File: AlternateVoiceFixture.cs
 * Date: 29.1.2018
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

using System;
using AlternateVoice.Server.Wrapper.Interfaces;
using Moq;
using NUnit.Framework;

namespace AlternateVoice.Server.Wrapper.Tests
{
    [TestFixture]
    public class AlternateVoiceFixture
    {
        [Test]
        public void MakingServerWillReturnNewVoiceServerInstance()
        {
            var repositoryMock = new Mock<IVoiceClientRepository>();
            
            var result = AlternateVoice.MakeServer(repositoryMock.Object, "localhost", 23332, 20);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IVoiceServer>(result);
        }
        
        [Test]
        public void GivingNullAsRepositoryWillThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => { AlternateVoice.MakeServer(null, "localhost", 23332, 20); });
        }
    }
}
