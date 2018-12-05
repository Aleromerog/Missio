using Domain;
using NUnit.Framework;

namespace DomainTests
{
    [TestFixture]
    public class StickyPostTests
    {
        private StickyPost _stickyPost;

        [SetUp]
        public void SetUp()
        {
            _stickyPost = new StickyPost("An author", "A message");
        }

        [Test]
        public void GetPostPriority_NormalState_ReturnsTen()
        {
            Assert.AreEqual(10, _stickyPost.GetPostPriority());
        }
    }
}