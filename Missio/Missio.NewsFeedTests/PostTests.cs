using NUnit.Framework;

namespace Missio.NewsFeedTests
{
    [TestFixture]
    public class PostTests
    {
        [Test]
        public void GetPostPriority_NormalState_ReturnsZero()
        {
            var post = Utils.MakeDummyPost();

            Assert.AreEqual(0, post.GetPostPriority());
        }
    }
}