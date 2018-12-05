using Domain;
using Missio;
using Missio.Navigation;
using NSubstitute;
using NUnit.Framework;
using ViewModels.Factories;

namespace ViewModelTests
{
    [TestFixture]
    public class NewsFeedPageFactoryTests
    {
        [Test]
        public void CreatePage_Should_ResolveNewsFeedPage()
        {
            var factory = new NewsFeedPageFactory(Substitute.For<INavigation>(), App.GetResolutionRoot("https://www.google.com.mx/"));
            var page = factory.CreatePage(new NameAndPassword("", ""));

            Assert.IsNotNull(page);
        }
    }
}