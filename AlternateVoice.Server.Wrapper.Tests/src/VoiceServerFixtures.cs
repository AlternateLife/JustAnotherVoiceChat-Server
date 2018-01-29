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
        public void StartingVoiceServerWillSetStartedPropertyToTrue()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);
            
            server.Start();
            
            
            
        }
        
    }
}
