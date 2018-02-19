/*
 * File: VoiceServerFixtures.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerFixtures
    {
        private Mock<IVoiceWrapper<IFakeVoiceClient>> _voiceWrapper;
        private Mock<IVoiceClientFactory<IFakeVoiceClient, byte>> _voiceClientFactory;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new Mock<IVoiceWrapper<IFakeVoiceClient>>();
            _voiceClientFactory = new Mock<IVoiceClientFactory<IFakeVoiceClient, byte>>();
        }

        [TearDown]
        public void TearDown()
        {
            _voiceWrapper = null;
            _voiceClientFactory = null;

            GC.Collect();
        }

        [Test]
        public void ConstructorWillThrowExceptionsWhenAParameterIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer<IFakeVoiceClient, byte>(null, _voiceWrapper.Object, "localhost", 23332, 23);
            });
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, null, "localhost", 23332, 23);
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
                    var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, invalidhostname, 23332, 23);
                });
            }
        }

        [Test]
        public void VoiceServerVariablesAreCorrectlySetOnConstruction()
        {
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "voice.dummydomain.com", 22233, 321);
            
            Assert.AreEqual(server.Hostname, "voice.dummydomain.com");
            Assert.AreEqual(server.Port, 22233);
            Assert.AreEqual(server.ChannelId, 321);
            
            var server2 = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "voice.domaindummy.com", 33567, 576, 2f, 1f, 12d);
            
            Assert.AreEqual(server2.Hostname, "voice.domaindummy.com");
            Assert.AreEqual(server2.Port, 33567);
            Assert.AreEqual(server2.ChannelId, 576);
            
            Assert.AreEqual(server2.GlobalRollOffScale, 2f);
            Assert.AreEqual(server2.GlobalDistanceFactor, 1f);
            Assert.AreEqual(server2.GlobalMaxDistance, 12d);
            
        }

        [Test]
        public void StartingVoiceServerWillSetStartedPropertyToTrueAndTriggerEvent()
        {
            var fakeWrapper = new Mock<IVoiceWrapper<IFakeVoiceClient>>();
            fakeWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            fakeWrapper.Setup(e => e.CreateNativeServer(23332));
            
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, fakeWrapper.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;
            
            server.Start();
            
            fakeWrapper.Verify(e => e.StartNativeServer(), Times.Once);
            fakeWrapper.Verify(e => e.CreateNativeServer(23332), Times.Once);
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(true, server.Started);
        }

        [Test]
        public void StoppingVoiceServerWillSetStartedPropertyToFalseAndTriggerEvent()
        {
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "localhost", 23332, 23);

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
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "localhost", 23332, 23);

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
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "localhost", 23332, 23);

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
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "localhost", 23332, 23);

            Assert.Throws<VoiceServerNotStartedException>(() =>
            {
                server.Stop();
            });
        }

        [Test]
        public void RegisterEventWillCallRegisterEventAndAddsInstanceToGarbageCollection()
        {
            var invokeAmount = 0;
            var givenNumber = 0;

            void Callback(int e)
            {
                givenNumber = e;
                invokeAmount++;
            }

            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, _voiceWrapper.Object, "localhost", 23332, 23);
            
            server.RegisterEvent(Callback, 5);
            
            Assert.AreEqual(invokeAmount, 1);
            Assert.AreEqual(givenNumber, 5);
        }
        
    }
}
