using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerClientFixtures
    {

        private FakeVoiceServer _voiceServer;
        
        private Mock<IVoiceWrapper<IFakeVoiceClient>> _voiceWrapper;

        [SetUp]
        public void SetUp()
        {
            _voiceWrapper = new Mock<IVoiceWrapper<IFakeVoiceClient>>();
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
            var client1 = _voiceServer.PrepareClient(5);
            
            //Assert.AreSame(client1, _voiceServer.FindClient(c => .c);
        }
        
    }
}
