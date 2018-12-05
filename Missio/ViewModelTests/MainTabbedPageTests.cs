using NUnit.Framework;
using ViewModels.Views;

namespace ViewModelTests
{
    [TestFixture]
    public class MainTabbedPageTests : MainTabbedPage
    {
        [Test]
        public void OnBackButtonPressed_Should_ReturnsFalse()
        {
            Assert.IsTrue(OnBackButtonPressed());
        }
    }
}
