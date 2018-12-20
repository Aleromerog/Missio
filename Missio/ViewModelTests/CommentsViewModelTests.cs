using DomainTests;
using NUnit.Framework;
using ViewModels;

namespace ViewModelTests
{
    [TestFixture]
    public class CommentsViewModelTests
    {
        [Test]
        public void PostSetter_Should_FirePropertyChanged()
        {
            var wasEventCalled = false;
            var commentsViewModel = new CommentsViewModel();
            commentsViewModel.PropertyChanged += (s, e) => wasEventCalled = true;
            
            commentsViewModel.Post = Utils.MakeDummyPost();

            Assert.IsTrue(wasEventCalled);
        }
    }
}