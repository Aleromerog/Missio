using Missio.Posts;
using NUnit.Framework;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class TextOnlyPostTests
    {
        private TextOnlyPost _textOnlyPost;

        [SetUp]
        public void SetUp()
        {
            _textOnlyPost = new TextOnlyPost("An author", "A message");
        }

        [Test]
        public void GetPostPriority_NormalState_ReturnsZero()
        {
            Assert.AreEqual(0, _textOnlyPost.GetPostPriority());
        }
    }
}