using NUnit.Framework;

namespace Missio.MainViewTests
{
    [TestFixture]
    public class MainTabbedPageTests : MainTabbedPage
    {
        [Test]
        public void OnBackButtonPressed_NormalCall_ReturnsFalse()
        {
            Assert.IsTrue(OnBackButtonPressed());
        }
    }
}
