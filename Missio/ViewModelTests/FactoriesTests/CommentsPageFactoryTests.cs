using DomainTests;
using Missio;
using NUnit.Framework;
using ViewModels.Factories;

namespace ViewModelTests
{
    [TestFixture]
    public class CommentsPageFactoryTests
    {
        [Test]
        public void Resolve_Should_ResolvePublicationViewModel()
        {
            var resolutionRoot = App.GetResolutionRoot("https://www.google.com.mx/");
            var factory = new CommentsPageFactory(null, resolutionRoot);
            var page = factory.CreatePage(Utils.MakeDummyPost());

            Assert.IsNotNull(page);
        }
    }
}