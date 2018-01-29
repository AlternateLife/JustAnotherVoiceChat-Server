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
