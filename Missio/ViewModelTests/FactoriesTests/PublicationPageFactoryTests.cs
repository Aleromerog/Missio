using Domain;
using Missio;
using Ninject;
using NUnit.Framework;
using ViewModels;
using ViewModels.Factories;

namespace ViewModelTests
{
    [TestFixture]
    public class PublicationPageFactoryTests
    {
        [Test]
        public void Resolve_Should_ResolvePublicationViewModel()
        {
            var resolutionRoot = App.GetResolutionRoot("https://www.google.com.mx/");
            var factory = new PublicationPageFactory(null, resolutionRoot);
            var arguments = factory.GetArguments(new NameAndPassword("", ""), () => {});

            Assert.DoesNotThrow(() => resolutionRoot.Get<PublicationPageViewModel>(arguments));
        }
    }
}