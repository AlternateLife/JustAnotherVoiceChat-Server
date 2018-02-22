/*
 * File: VoiceServerFixtures.cs
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

using System;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes.Interfaces;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerFixtures
    {
        private Mock<IVoiceWrapper> _voiceWrapper;
        private Mock<IVoiceClientFactory<IFakeVoiceClient, byte>> _voiceClientFactory;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new Mock<IVoiceWrapper>();
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
                var server = new VoiceServer<IFakeVoiceClient, byte>(null, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123", 1, 1, 6), _voiceWrapper.Object);
            });
        }

        [Test]
        public void ConstructorWithInvalidHostnameWillThrowInvalidHostnameException()
        {
            var invalidhostnames = new [] { "ä#äää", "" };

            foreach (var invalidhostname in invalidhostnames)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration(invalidhostname, 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);
                });
            }
        }

        [Test]
        public void VoiceServerVariablesAreCorrectlySetOnConstruction()
        {
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("voice.domaindummy.com", 33567, "Identit3y7rrV3RYNiC3EEEEEwgeA=", 987, "verySecurePassword", 2f, 1f, 12d), _voiceWrapper.Object);

            var config = server.Configuration;
            
            Assert.AreEqual(config.Hostname, "voice.domaindummy.com");
            Assert.AreEqual(config.Port, 33567);
            Assert.AreEqual(config.TeamspeakServerId, "Identit3y7rrV3RYNiC3EEEEEwgeA=");
            Assert.AreEqual(config.TeamspeakChannelId, 987);
            Assert.AreEqual(config.TeamspeakChannelPassword, "verySecurePassword");
            
            Assert.AreEqual(config.GlobalRollOffScale, 2f);
            Assert.AreEqual(config.GlobalDistanceFactor, 1f);
            Assert.AreEqual(config.GlobalMaxDistance, 12d);
            
        }

        [Test]
        public void StartingVoiceServerWillSetStartedPropertyToTrueAndTriggerEvent()
        {
            var configuration = new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123");
            
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            _voiceWrapper.Setup(e => e.CreateNativeServer(configuration));
            
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, configuration, _voiceWrapper.Object);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;
            
            server.Start();
            
            _voiceWrapper.Verify(e => e.StartNativeServer(), Times.Once);
            _voiceWrapper.Verify(e => e.CreateNativeServer(configuration), Times.Once);
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(true, server.Started);
        }

        [Test]
        public void StoppingVoiceServerWillSetStartedPropertyToFalseAndTriggerEvent()
        {
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);

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
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);

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
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);

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
            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);

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

            var server = new VoiceServer<IFakeVoiceClient, byte>(_voiceClientFactory.Object, new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);
            
            server.RegisterEvent(Callback, 5);
            
            Assert.AreEqual(invokeAmount, 1);
            Assert.AreEqual(givenNumber, 5);
        }
        
    }
}
