/*
 * File: VoiceServerClientFixtures.cs
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
using System.Linq;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerClientFixtures
    {

        private FakeVoiceServer _voiceServer;
        
        private Mock<IVoiceWrapper> _voiceWrapper;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new Mock<IVoiceWrapper>();
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            
            _voiceServer = new FakeVoiceServer(new FakeVoiceClientFactory(), new VoiceServerConfiguration("localhost", 23332, "Identit3y7rrV3RYNiC3MnupTwgeA=", 130, "123"), _voiceWrapper.Object);
            _voiceServer.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
            _voiceServer = null;

            _voiceWrapper = null;
        }

        [Test]
        public void PrepareClientWillRegisterANewClientToServer()
        {
            var client = _voiceServer.PrepareClient(5);
            
            Assert.NotNull(client);
            Assert.IsInstanceOf<FakeVoiceClient>(client);
            Assert.AreEqual(5, client.Identifer);

            _voiceServer.RemoveClient(client);
        }

        [Test]
        public void VoiceHandleWillIncreaseWithEveryPreparedClient()
        {
            for (byte i = 1; i < 10; i++)
            {
                var client = _voiceServer.PrepareClient(1);

                Assert.AreEqual(i, client.Handle.Identifer);
            }
        }

        [Test]
        public void NewVoiceHandleWillFillEmptySpaces()
        {
            var client1 = _voiceServer.PrepareClient(1);
            var client2 = _voiceServer.PrepareClient(2);
            var client3 = _voiceServer.PrepareClient(3);
            var client4 = _voiceServer.PrepareClient(4);

            _voiceServer.RemoveClient(client3);
            var fillClient1 = _voiceServer.PrepareClient(5);

            Assert.AreEqual(3, fillClient1.Handle.Identifer);

            var fillClient2 = _voiceServer.PrepareClient(6);

            Assert.AreEqual(5, fillClient2.Handle.Identifer);
        }

        [Test]
        public void PreparedClientWillBeSearchableInGetClients()
        {
            for (byte i = 0; i < 10; i = (byte) (i + 2))
            {
                _voiceServer.PrepareClient(i);
            }
            
            var client = _voiceServer.PrepareClient(100);
            
            Assert.NotNull(client);
            Assert.AreSame(client, _voiceServer.FindClient(c => c.Identifer == 100));
        }

        [Test]
        public void PreparedAndRemovedClientIsRemovedFromKnownClients()
        {
            var client = _voiceServer.PrepareClient(5);
            _voiceServer.RemoveClient(client);
            
            Assert.Null(_voiceServer.FindClient(c => c.Identifer == 5));

            
            var overwriteClient = _voiceServer.PrepareClient(5);
            
            Assert.AreNotSame(client, _voiceServer.FindClient(c => c.Identifer == 5));
            Assert.AreSame(overwriteClient, _voiceServer.FindClient(c => c.Identifer == 5));
        }

        [Test]
        public void FindingClientWithVariousWays()
        {
            var client = _voiceServer.PrepareClient(10);
            
            Assert.AreSame(client, _voiceServer.GetVoiceClient(client.Handle));
            Assert.AreSame(client, _voiceServer.GetVoiceClient(client.Handle.Identifer));
            Assert.IsTrue(_voiceServer.GetClients().Any(c => ReferenceEquals(c, client)));
        }

        [Test]
        public void RemovingNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                _voiceServer.RemoveClient(null);
            });
        }

        [Test]
        public void RemoveNonExistentClientThrowsInvalidClientException()
        {
            var client = _voiceServer.PrepareClient(5);
            _voiceServer.RemoveClient(client);
            
            var fakeClient = new FakeVoiceClient(10, _voiceServer, VoiceHandle.Empty);
            
            Assert.Throws<InvalidClientException>(() => { _voiceServer.RemoveClient(client); });
            Assert.Throws<InvalidClientException>(() => { _voiceServer.RemoveClient(fakeClient); });
        }

        [Test]
        public void MuteForNullThrowsArgumentNullException()
        {
            var client = _voiceServer.PrepareClient(49);

            Assert.Throws<ArgumentNullException>(() => { client.MuteSpeaker(null, true); });
        }

        [Test]
        public void IsMutedForNullThrowsArgumentNullException()
        {
            var client = _voiceServer.PrepareClient(1);

            Assert.Throws<ArgumentNullException>(() => { client.IsSpeakerMuted(null); });
        }

        [Test]
        public void MuteForAllTriggersWrapper()
        {
            var client = _voiceServer.PrepareClient(2);
            var wrapperTriggered = false;
            _voiceWrapper.Setup(wrapper => wrapper.MuteClientForAll(client, true)).Callback(() => wrapperTriggered = true);

            client.MuteForAll(true);

            Assert.IsTrue(wrapperTriggered);
        }

        [Test]
        public void IsMutedForAllTriggersWrapper()
        {
            var client = _voiceServer.PrepareClient(2);
            var wrapperTriggered = false;
            _voiceWrapper.Setup(wrapper => wrapper.IsClientMutedForAll(client)).Callback(() => wrapperTriggered = true);

            client.IsMutedForAll();

            Assert.IsTrue(wrapperTriggered);
        }

        [Test]
        public void MuteSpeakerTriggersNativeWrapper()
        {
            var listener = _voiceServer.PrepareClient(2);
            var speaker = _voiceServer.PrepareClient(3);
            var wrapperTriggered = false;
            _voiceWrapper.Setup(wrapper => wrapper.MuteClientForClient(speaker, listener, true)).Callback(() => wrapperTriggered = true);

            listener.MuteSpeaker(speaker, true);

            Assert.IsTrue(wrapperTriggered);
        }

        [Test]
        public void IsSpeakerMutedTriggersNativeWrapper()
        {
            var listener = _voiceServer.PrepareClient(2);
            var speaker = _voiceServer.PrepareClient(3);
            var wrapperTriggered = false;
            _voiceWrapper.Setup(wrapper => wrapper.IsClientMutedForClient(speaker, listener)).Callback(() => wrapperTriggered = true);

            listener.IsSpeakerMuted(speaker);

            Assert.IsTrue(wrapperTriggered);
        }

    }
}
