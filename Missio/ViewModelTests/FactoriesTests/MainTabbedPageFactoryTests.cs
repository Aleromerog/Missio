using Domain;
using Missio;
using NSubstitute;
using NUnit.Framework;
using ViewModels.Factories;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModelTests
{
    [TestFixture]
    public class MainTabbedPageFactoryTests
    {
        [Test]
        public void Resolve_Should_ResolveMainTabbedPage()
        {
            var factory = new MainTabbedPageFactory(Substitute.For<INavigation>(), App.GetResolutionRoot("https://www.google.com.mx/"));
            var page = factory.CreatePage(new NameAndPassword("", ""));

            Assert.IsNotNull(page);
        }
    }
}