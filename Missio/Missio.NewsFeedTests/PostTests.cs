using Missio.Posts;
using NUnit.Framework;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class PostTests
    {
        private Post _post;

        [SetUp]
        public void SetUp()
        {
            _post = new Post();
        }

        [Test]
        public void GetPostPriority_NormalState_ReturnsZero()
        {
            Assert.AreEqual(0, _post.GetPostPriority());
        }
    }
}