/*
 * File: VoiceServerFixture.cs
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

using System;
using AlternateVoice.Server.Wrapper.Elements.Server;
using AlternateVoice.Server.Wrapper.Exceptions;
using AlternateVoice.Server.Wrapper.Interfaces;
using Moq;
using NUnit.Framework;

namespace AlternateVoice.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerFixtures
    {

        [Test]
        public void ConstructorWillThrowExceptionIfRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer(null, "localhost", 23332, 23);
            });
        }

        [Test]
        public void ConstructorWithInvalidHostnameWillThrowInvalidHostnameException()
        {
            var repository = new Mock<IVoiceClientRepository>();
            
            var invalidhostnames = new [] { "ä#äää", "" };

            foreach (var invalidhostname in invalidhostnames)
            {
                Assert.Throws<InvalidHostnameException>(() =>
                {
                    var server = new VoiceServer(repository.Object, invalidhostname, 23332, 23);
                });
            }
        }

        [Test]
        public void StartingVoiceServerWillSetStartedPropertyToTrueAndTriggerEvent()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;
            
            server.Start();
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(true, server.Started);
        }

        [Test]
        public void StoppingVoiceServerWillSetStartedPropertyToFalseAndTriggerEvent()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

            Assert.Throws<VoiceServerNotStartedException>(() =>
            {
                server.Stop();
            });
        }
        
    }
}
