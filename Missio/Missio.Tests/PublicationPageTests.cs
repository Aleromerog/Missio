using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    [Category("UITests")]
    public class PublicationPageTests
    {
        private IApp app;
        private readonly Platform platform;

        public PublicationPageTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void SetUp()
        {
            app = AppInitializer.StartApp(platform);
            app.LogInWithDefaultUser();
            app.Tap(c => c.Button("AddPostButton"));
        }

        [Test]
        public void PublishButton_GivenPostContent_PublishesPost()
        {
            //Arrange
            var postText = "Content of the new post";
            //Act
            app.EnterText(c => c.TextField("PostTextEntry"), postText);
            app.Tap(c => c.Button("PublishPostButton"));
            //Assert
            app.WaitForElement(c => c.Marked("NewsFeedPage"));
            app.WaitForElement(c => c.Text(postText));
        }
    }
}