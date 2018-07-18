using Missio.Tests;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.PostPublicationTests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class PublicationPageUserInterfaceTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public PublicationPageUserInterfaceTests(Platform platform)
        {
            this._platform = platform;
        }

        [SetUp]
        public void SetUp()
        {
            _app = AppInitializer.StartApp(_platform);
            _app.LogInWithDefaultUser();
            _app.Tap(c => c.Button("AddPostButton"));
        }

        [Test]
        public void PublishButton_GivenPostContent_PublishesPost()
        {
            var postText = "Content of the new post";

            _app.EnterText(c => c.TextField("PostTextEntry"), postText);
            _app.Tap(c => c.Button("PublishPostButton"));

            _app.WaitForElement(c => c.Marked("NewsFeedPage"));
            _app.WaitForElement(c => c.Text(postText));
        }
    }
}