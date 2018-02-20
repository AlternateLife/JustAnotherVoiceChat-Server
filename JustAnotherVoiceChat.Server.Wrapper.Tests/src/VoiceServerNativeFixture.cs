using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes.Interfaces;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerNativeFixture
    {

        private FakeVoiceServer _voiceServer;
        private FakeVoiceClientFactory _voiceClientFactory;
        private Mock<IVoiceWrapper<IFakeVoiceClient>> _voiceWrapper;

        [SetUp]
        public void SetUp()
        {
            _voiceClientFactory = new FakeVoiceClientFactory();
            _voiceWrapper = new Mock<IVoiceWrapper<IFakeVoiceClient>>();
            
            _voiceServer = new FakeVoiceServer(_voiceClientFactory, "localhost", 23332, 123, _voiceWrapper.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (_voiceServer.Started)
            {
                _voiceServer.Stop();
            }
            
            _voiceServer.Dispose();

            _voiceServer = null;
            _voiceClientFactory = null;
            _voiceWrapper = null;
        }

        private void StartServer()
        {
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);
            _voiceServer.Start();
        }

        [Test]
        public void CreatingAServerCreatesANativeServerWithPort()
        {
            _voiceWrapper.Setup(e => e.CreateNativeServer(23123));
            
            var tmpServer = new FakeVoiceServer(_voiceClientFactory, "localhost", 23123, 232, _voiceWrapper.Object);
            
            _voiceWrapper.Verify(e => e.CreateNativeServer(23123), Times.Once);
        }

        [Test]
        public void StartingANewServerWillStartAndCheckNativeServer()
        {
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(true);

            var startEventAmount = 0;
            _voiceServer.OnServerStarted += () => startEventAmount++;
            
            _voiceServer.Start();
            
            _voiceWrapper.Verify(e => e.StartNativeServer(), Times.Once);
            Assert.AreEqual(1, startEventAmount);
            Assert.True(_voiceServer.Started);
        }

        [Test]
        public void StartingInvalidServerWillThrowException()
        {
            _voiceWrapper.Setup(e => e.StartNativeServer()).Returns(false);
            
            var startEventAmount = 0;
            _voiceServer.OnServerStarted += () => startEventAmount++;

            Assert.Throws<VoiceServerNotStartedException>(() => _voiceServer.Start());
            
            _voiceWrapper.Verify(e => e.StartNativeServer(), Times.Once);
            
            Assert.AreEqual(0, startEventAmount);
            Assert.False(_voiceServer.Started);
        }

        [Test]
        public void StoppingVoiceServerWillStopNativeServer()
        {
            StartServer();

            _voiceWrapper.Setup(e => e.StopNativeServer());
            
            _voiceServer.Stop();
            
            _voiceWrapper.Verify(e => e.StopNativeServer(), Times.Once);
        }

        [Test]
        public void StoppingNotStartedServerWillThrowException()
        {
            _voiceWrapper.Setup(e => e.StopNativeServer());
            
            Assert.Throws<VoiceServerNotStartedException>(() =>
            {
                _voiceServer.Stop();
            });
            
            _voiceWrapper.Verify(e => e.StopNativeServer(), Times.Never);
        }
        
        
        
    }
}
