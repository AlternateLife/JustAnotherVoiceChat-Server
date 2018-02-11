/*
 * File: VoiceServerFixtures.cs
 * Date: 11.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 Alternate-Life.de
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
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerFixtures
    {
        private Mock<IVoiceWrapper> _voiceWrapper;
        private Mock<IVoiceWrapper3D> _voiceWrapper3D;
        private Mock<IVoiceClientRepository> _clientRepoMock;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new Mock<IVoiceWrapper>();
            _voiceWrapper3D = new Mock<IVoiceWrapper3D>();
            _clientRepoMock = new Mock<IVoiceClientRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _voiceWrapper = null;
            _voiceWrapper3D = null;
            _clientRepoMock = null;

            GC.Collect();
        }

        [Test]
        public void ConstructorWillThrowExceptionIfRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer(null, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);
            });
        }

        [Test]
        public void ConstructorWillThrowExceptionIfWrapperIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer(_clientRepoMock.Object, null, _voiceWrapper3D.Object, "localhost", 23332, 23);
            });
        }

        [Test]
        public void ConstructorWillThrowExceptionIfWrapper3DIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, null, "localhost", 23332, 23);
            });
        }

        [Test]
        public void ConstructorWithInvalidHostnameWillThrowInvalidHostnameException()
        {
            var invalidhostnames = new [] { "ä#äää", "" };

            foreach (var invalidhostname in invalidhostnames)
            {
                Assert.Throws<InvalidHostnameException>(() =>
                {
                    var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, invalidhostname, 23332, 23);
                });
            }
        }

        [Test]
        public void StartingVoiceServerWillSetStartedPropertyToTrueAndTriggerEvent()
        {
            var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;
            
            server.Start();
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(true, server.Started);
        }

        [Test]
        public void StoppingVoiceServerWillSetStartedPropertyToFalseAndTriggerEvent()
        {
            var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStopping += () => invokeAmount++;
            
            server.Start();
            server.Stop();
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(false, server.Started);
        }

        [Test]
        public void StartingVoiceServerMultipleWillTriggerEventOnlyIfServerIsNotStarted()
        {
            var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;

            void ServerStart()
            {
                server.Start();
            }

            for (var i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    Assert.DoesNotThrow(ServerStart);
                }
                else
                {
                    Assert.Throws<VoiceServerAlreadyStartedException>(ServerStart);
                }
            }
            
            Assert.AreEqual(1, invokeAmount);
        }

        [Test]
        public void StartingAndStoppingServerWillTriggerEventMultipleTimes()
        {
            var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);

            var startInvokeAmount = 0;
            server.OnServerStarted += () => startInvokeAmount++;
            
            var stopInvokeAmount = 0;
            server.OnServerStopping += () => stopInvokeAmount++;

            for (var i = 0; i < 5; i++)
            {
                Assert.DoesNotThrow(() =>
                {
                    server.Start();
                    server.Stop();
                });
            }
            
            Assert.AreEqual(5, startInvokeAmount);
            Assert.AreEqual(5, stopInvokeAmount);
        }

        [Test]
        public void StoppingServerWithoutStartingItFirstWillThrowAnException()
        {
            var server = new VoiceServer(_clientRepoMock.Object, _voiceWrapper.Object, _voiceWrapper3D.Object, "localhost", 23332, 23);

            Assert.Throws<VoiceServerNotStartedException>(() =>
            {
                server.Stop();
            });
        }
        
    }
}
