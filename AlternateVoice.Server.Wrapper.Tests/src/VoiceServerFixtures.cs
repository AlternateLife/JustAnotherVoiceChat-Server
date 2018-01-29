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
        public void StartingVoiceServerWillSetStartedPropertyToTrueAndTriggerEvent()
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
        public void StoppingVoiceServerWillSetStartedPropertyToFalseAndTriggerEvent()
        {
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

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
            var repository = new Mock<IVoiceClientRepository>();
            var server = new VoiceServer(repository.Object, "localhost", 23332, 23);

            Assert.Throws<VoiceServerNotStartedException>(() =>
            {
                server.Stop();
            });
        }
        
    }
}
