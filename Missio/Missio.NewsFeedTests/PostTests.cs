using Missio.Posts;
using Missio.UserTests;
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
            _post = new Post(UserTestUtils.FranciscoUser, "A message");
        }

        [Test]
        public void GetPostPriority_NormalState_ReturnsZero()
        {
            Assert.AreEqual(0, _post.GetPostPriority());
        }
    }
}