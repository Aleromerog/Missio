using System.Threading.Tasks;
using Missio.Navigation;
using Missio.PostPublication;
using Missio.Posts;
using Missio.Users;
using NSubstitute;
using NUnit.Framework;

namespace Missio.PostPublicationTests
{
    [TestFixture]
    public class PublicationPageViewModelTests
    {
        private PublicationPageViewModel MakePublicationPageViewModel(IPostRepository postRepository, NameAndPassword nameAndPassword, IUpdateViewPosts updateViewPosts, INavigation navigation)
        {
            return new PublicationPageViewModel(postRepository, nameAndPassword, updateViewPosts, navigation);
        }

        [Test]
        public async Task PublishPost_TextOnly_UpdatesPostsView()
        {
            var updatePostsView = Substitute.For<IUpdateViewPosts>();
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), new NameAndPassword("", ""), updatePostsView, Substitute.For<INavigation>());
            await publicationPageViewModel.PublishPost();

            await updatePostsView.Received(1).UpdatePosts();
        }

        [Test]
        public async Task PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            var navigation = Substitute.For<INavigation>();
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), new NameAndPassword("", ""), Substitute.For<IUpdateViewPosts>(), navigation);

            await publicationPageViewModel.PublishPost();

            await navigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public async Task PublishPostCommand_TextOnly_PublishesPost()
        {
            var nameAndPassword = new NameAndPassword("Francisco Greco", "ElPass");
            var newPostText = "The content of the new post";
            var postRepository = Substitute.For<IPostRepository>();
            var publicationPageViewModel = MakePublicationPageViewModel(postRepository, nameAndPassword, Substitute.For<IUpdateViewPosts>(), Substitute.For<INavigation>());
            publicationPageViewModel.PostText = newPostText;
            await publicationPageViewModel.PublishPost();

            await postRepository.Received().PublishPost(Arg.Is<CreatePostDTO>(x => x.Message == newPostText && x.NameAndPassword == nameAndPassword));
        }
    }
}