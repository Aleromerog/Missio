using System.Threading.Tasks;
using JetBrains.Annotations;
using Missio;
using Ninject.Parameters;
using Ninject.Syntax;
using NSubstitute;
using NUnit.Framework;
using ViewModels.Factories;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModelTests
{
    [TestFixture]
    public class PageFactoryOneParameterTests
    {
        [Test]
        public void CreatePage_Should_ResolveMockPage()
        {
            var factory = new MockFactory(Substitute.For<INavigation>(), App.GetResolutionRoot("https://www.google.com.mx/"));
            var page = factory.CreatePage(9);

            Assert.IsNotNull(page);
        }

        [Test]
        public async Task CreateAndNavigateToPage_Should_NavigateToPage()
        {
            var navigation = Substitute.For<INavigation>();
            var factory = new MockFactory(navigation, App.GetResolutionRoot("https://www.google.com.mx/"));
            var page = await factory.CreateAndNavigateToPage(10);

            await navigation.Received().GoToPage(page);
        }

        private class MockFactory : PageFactory<MockPage, int>
        {
            /// <inheritdoc />
            public MockFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
            {
            }

            /// <inheritdoc />
            protected override IParameter[] GetArguments(int parameter)
            {
                return new IParameter[] { new ConstructorArgument("value", 9) };
            }
        }

        private class MockPage : Page
        {
            [UsedImplicitly]
            public MockPage(int value)
            {
            }
        }
    }
}