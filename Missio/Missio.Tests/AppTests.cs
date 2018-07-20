using NUnit.Framework;

namespace Missio.Tests
{
    [TestFixture]
    public class AppTests
    {
        [Test]
        public void Constructor_NormalConstructor_DoesNotThrowExceptions()
        {
            Assert.IsNotNull(App.ResolveApplicationNavigation());
        }
    }
}