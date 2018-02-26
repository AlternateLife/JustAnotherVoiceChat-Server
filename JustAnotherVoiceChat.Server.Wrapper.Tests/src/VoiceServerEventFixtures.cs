/*
 * File: VoiceServerEventFixtures.cs
 * Date: 26.2.2018,
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
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes.Interfaces;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerEventFixtures
    {

        private VoiceWrapperEventInvoker _voiceWrapper;
        private FakeVoiceServer _voiceServer;

        private IFakeVoiceClient _voiceClient;
        
        private int _clientConnectingInvokes;
        private int _clientConnectedInvokes;
        private int _clientDisconnectedInvokes;
        private int _clientSpeakersMuteInvokes;
        private int _clientMicrophoneMuteInvokes;
        private int _clientRejectedInvokes;
        private int _clientTalkingChangedInvokes;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new VoiceWrapperEventInvoker();
            _voiceServer = new FakeVoiceServer(new FakeVoiceClientFactory(), new VoiceServerConfiguration("localhost", 23332, "jfdsjlsdflk==", 123, "secret"), _voiceWrapper);

            _voiceClient = _voiceServer.PrepareClient(1);
            
            _voiceServer.OnClientConnecting += (client, id, args) => _clientConnectingInvokes++;
            _voiceServer.OnClientConnected += client => _clientConnectedInvokes++;
            _voiceServer.OnClientDisconnected += client => _clientDisconnectedInvokes++;
            _voiceServer.OnClientSpeakersMuteChanged += (client, muted) => _clientSpeakersMuteInvokes++;
            _voiceServer.OnClientMicrophoneMuteChanged += (client, muted) => _clientMicrophoneMuteInvokes++;
            _voiceServer.OnClientRejected += (client, code) => _clientRejectedInvokes++;
            _voiceServer.OnClientTalkingChanged += (client, status) => _clientTalkingChangedInvokes++;
        }

        [TearDown]
        public void TearDown()
        {
            _voiceServer.RemoveClient(_voiceClient);
            _voiceClient = null;
            
            _voiceServer.Dispose();
            _voiceServer = null;

            _voiceWrapper = null;
            
            _clientConnectingInvokes = 0;
            _clientConnectedInvokes = 0;
            _clientDisconnectedInvokes = 0;
            _clientSpeakersMuteInvokes = 0;
            _clientMicrophoneMuteInvokes = 0;
            _clientRejectedInvokes = 0;
            _clientTalkingChangedInvokes = 0;
        }

        [Test]
        public void ClientMuteNativeChangesWillSetClientProperties()
        {
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            Assert.IsTrue(_voiceClient.Microphone);
            Assert.IsTrue(_voiceClient.Speakers);
            
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, true);
            
            Assert.IsFalse(_voiceClient.Microphone);
            Assert.IsFalse(_voiceClient.Speakers);
            
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, false);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, false);
            
            Assert.IsTrue(_voiceClient.Microphone);
            Assert.IsTrue(_voiceClient.Speakers);
            
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(2, true);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, false);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(2, false);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(2, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, false);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(2, false);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, true);
            
            Assert.IsFalse(_voiceClient.Microphone);
            Assert.IsFalse(_voiceClient.Speakers);
        }

        [Test]
        public void ClientConnectedWillChangeClientProperty()
        {
            Assert.IsFalse(_voiceClient.Connected);
            
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            Assert.IsTrue(_voiceClient.Connected);
            
            _voiceWrapper.InvokeClientDisconnectedCallback(1);
            
            Assert.IsFalse(_voiceClient.Connected);
        }

        [Test]
        public void ServerWillRejectNotPreparedClient()
        {
            var resultNotPrepared = _voiceWrapper.InvokeClientConnectingCallback(2, "identity=");
            
            Assert.IsFalse(resultNotPrepared);

            var preparedClient = _voiceServer.PrepareClient(2);
            var resultPrepared = _voiceWrapper.InvokeClientConnectingCallback(2, "identity=");
            
            Assert.NotNull(preparedClient);
            Assert.IsTrue(resultPrepared);
        }

        [Test]
        public void ClientConnectWillOnlyInvokeOnNotConnectedClients()
        {
            _voiceWrapper.InvokeClientConnectingCallback(1, "jsdfjsd1");
            _voiceWrapper.InvokeClientConnectingCallback(2, "jsdfjsd2");
            _voiceWrapper.InvokeClientConnectingCallback(3, "jsdfjsd3");
            _voiceWrapper.InvokeClientConnectingCallback(1, "jsdfjsd4");
            
            _voiceWrapper.InvokeClientConnectedCallback(1);
            _voiceWrapper.InvokeClientConnectedCallback(2);
            _voiceWrapper.InvokeClientConnectedCallback(3);
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            Assert.AreEqual(2, _clientConnectingInvokes);
            Assert.AreEqual(1, _clientConnectedInvokes);
        }

        [Test]
        public void ClientDisconnectedWillOnlyInvokeOnConnectedClients()
        {
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            _voiceWrapper.InvokeClientDisconnectedCallback(1);
            _voiceWrapper.InvokeClientDisconnectedCallback(1);
            _voiceWrapper.InvokeClientDisconnectedCallback(1);
            
            _voiceWrapper.InvokeClientConnectedCallback(2);
            
            _voiceWrapper.InvokeClientDisconnectedCallback(2);
            
            Assert.AreEqual(1, _clientDisconnectedInvokes);
        }

        [Test]
        public void MicrophoneChangesAreOnlyTriggeredWhenPlayerIsConnected()
        {
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(2, true);
            _voiceWrapper.InvokeClientMicrophoneMuteChangedCallback(1, false);
            
            Assert.AreEqual(2, _clientMicrophoneMuteInvokes);
            
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(2, true);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, false);
            _voiceWrapper.InvokeClientSpeakersMuteChangedCallback(1, true);
            
            Assert.AreEqual(3, _clientSpeakersMuteInvokes);
        }

        [Test]
        public void ServerSendsClientTalkingChangedtoConnectedClients()
        {
            _voiceWrapper.InvokeClientConnectedCallback(1);
            
            _voiceWrapper.InvokeClientTalkingChangedCallback(1, true);
            _voiceWrapper.InvokeClientTalkingChangedCallback(1, false);
            
            _voiceWrapper.InvokeClientTalkingChangedCallback(2, true);
            _voiceWrapper.InvokeClientTalkingChangedCallback(2, false);
            
            Assert.AreEqual(2, _clientTalkingChangedInvokes);
        }

        [Test]
        public void ServerSendsClientRejectedEvent()
        {
            _voiceWrapper.InvokeClientRejectedCallback(1, (int) StatusCode.NotConnectedToServer);
            _voiceWrapper.InvokeClientRejectedCallback(2, (int) StatusCode.NotConnectedToServer);
            
            Assert.AreEqual(1, _clientRejectedInvokes);
        }

        [Test]
        public void RunWhenValidWillTriggerOnPreparedClients()
        {
            var validInvokes = 0;
            var invalidInvokes = 0;

            _voiceServer.RunWhenClientValid(1, client =>
            {
                validInvokes++;
            });
            
            _voiceServer.RunWhenClientValid(2, client =>
            {
                invalidInvokes++;
            });
            
            Assert.AreEqual(1, validInvokes);
            Assert.AreEqual(0, invalidInvokes);
        }

        [Test]
        public void RunWhenConnectedWillTriggerOnConnectedPreparedClients()
        {
            var validInvokes = 0;
            var invalidInvokes = 0;

            _voiceServer.RunWhenClientConnected(1, client =>
            {
                invalidInvokes++;
            });
            
            _voiceServer.RunWhenClientConnected(2, client =>
            {
                invalidInvokes++;
            });
            
            Assert.AreEqual(0, validInvokes);
            Assert.AreEqual(0, invalidInvokes);

            validInvokes = 0;
            invalidInvokes = 0;
            
            _voiceWrapper.InvokeClientConnectedCallback(1);

            _voiceServer.RunWhenClientConnected(1, client =>
            {
                validInvokes++;
            });

            _voiceServer.RunWhenClientConnected(2, client =>
            {
                invalidInvokes++;
            });
            
            Assert.AreEqual(1, validInvokes);
            Assert.AreEqual(0, invalidInvokes);
        }

        [Test]
        public void GenericRunWhenValidWillReturnInnerValue()
        {
            var result1 = _voiceServer.RunWhenClientValid(1, c => 1234);
            var result2 = _voiceServer.RunWhenClientValid(1, c => true);
            var result3 = _voiceServer.RunWhenClientValid(1, c => false);
            var result4 = _voiceServer.RunWhenClientValid(1, c => new DateTime(2018, 10, 1));
            
            Assert.AreEqual(1234, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(new DateTime(2018, 10, 1), result4);
        }

        [Test]
        public void GenericRunWhenValidWillDefaultValueIfInvalidClient()
        {
            var result1 = _voiceServer.RunWhenClientValid(2, c => 1234);
            var result2 = _voiceServer.RunWhenClientValid(2, c => true);
            var result3 = _voiceServer.RunWhenClientValid(2, c => false);
            var result4 = _voiceServer.RunWhenClientValid(2, c => new DateTime(2018, 10, 1));
            
            Assert.AreEqual(default(int), result1);
            Assert.AreEqual(default(bool), result2);
            Assert.AreEqual(default(bool), result3);
            Assert.AreEqual(default(DateTime), result4);
        }
        
    }
}
