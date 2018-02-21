using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes;
using Moq;
using NUnit.Framework;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    [TestFixture]
    public class VoiceServerNativeFixture
    {

        private FakeVoiceServer _voiceServer;
        private FakeVoiceClientFactory _voiceClientFactory;
        private Mock<IVoiceWrapper> _voiceWrapper;

        [SetUp]
        public void SetUp()
        {
            _voiceClientFactory = new FakeVoiceClientFactory();
            _voiceWrapper = new Mock<IVoiceWrapper>();
            
            _voiceServer = new FakeVoiceServer(_voiceClientFactory, new VoiceServerConfiguration("voice.domaindummy.com", 33567, "Identit3y7rrV3RYNiC3EEEEEwgeA=", 987, "verySecurePassword"), _voiceWrapper.Object);
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
            var configuration = new VoiceServerConfiguration("voice.domaindummy.com", 33567, "Identit3y7rrV3RYNiC3EEEEEwgeA=", 987, "verySecurePassword");
            _voiceWrapper.Setup(e => e.CreateNativeServer(configuration));
            
            var tmpServer = new FakeVoiceServer(_voiceClientFactory, configuration, _voiceWrapper.Object);
            
            _voiceWrapper.Verify(e => e.CreateNativeServer(configuration), Times.Once);
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
