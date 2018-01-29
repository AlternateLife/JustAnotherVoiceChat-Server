using AlternateVoice.Server.Wrapper.Interfaces;
using NUnit.Framework;

namespace AlternateVoice.Server.Wrapper.Tests
{
    [TestFixture]
    public class AlternateVoiceFixture
    {
        [Test]
        public void MakingServerWillReturnNewVoiceServerInstance()
        {
            var result = AlternateVoice.MakeServer(null, "localhost", 23332, 20);

            Assert.NotNull(result);
            Assert.IsInstanceOf<IVoiceServer>(result);
        }
    }
}
