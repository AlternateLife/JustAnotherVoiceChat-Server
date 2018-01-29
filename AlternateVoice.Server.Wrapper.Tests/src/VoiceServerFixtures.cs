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

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;
            
            server.Start();
            
            Assert.AreEqual(1, invokeAmount);
            Assert.AreEqual(true, server.Started);
        }

        [Test]
        public void StartingVoiceServerMultipleWillTriggerEventOnlyIfServerIsNotStarted()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;

            for (var i = 0; i < 5; i++)
            {
                server.Start();
            }
            
            Assert.AreEqual(1, invokeAmount);
        }

        [Test]
        public void StartingAndStoppingServerWillTriggerEventMultipleTimes()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

            var invokeAmount = 0;
            server.OnServerStarted += () => invokeAmount++;

            for (var i = 0; i < 5; i++)
            {
                server.Start();
                server.Stop();
            }
            
            Assert.AreEqual(5, invokeAmount);
        }
        
    }
}
