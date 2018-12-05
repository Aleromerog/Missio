using System;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using Domain.Repositories;
using Missio.Navigation;
using NSubstitute;
using NUnit.Framework;
using ViewModels;

namespace ViewModelTests
{
    [TestFixture]
    public class PublicationPageViewModelTests
    {
        private PublicationPageViewModel MakePublicationPageViewModel(IPostRepository postRepository, NameAndPassword nameAndPassword, Action postPublishedCallback, INavigation navigation)
        {
            return new PublicationPageViewModel(postRepository, nameAndPassword, postPublishedCallback, navigation);
        }

        [Test]
        public async Task PublishPost_TextOnly_FiresCallback()
        {
            var wasActionCalled = false;
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), new NameAndPassword("", ""), () => wasActionCalled = true, Substitute.For<INavigation>());
            await publicationPageViewModel.PublishPost();

            Assert.IsTrue(wasActionCalled);
        }

        [Test]
        public async Task PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            var navigation = Substitute.For<INavigation>();
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), new NameAndPassword("", ""), () => {}, navigation);

            await publicationPageViewModel.PublishPost();

            await navigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public async Task PublishPostCommand_TextOnly_PublishesPost()
        {
            var nameAndPassword = new NameAndPassword("Francisco Greco", "ElPass");
            var newPostText = "The content of the new post";
            var postRepository = Substitute.For<IPostRepository>();
            var publicationPageViewModel = MakePublicationPageViewModel(postRepository, nameAndPassword, () => {}, Substitute.For<INavigation>());
            publicationPageViewModel.PostText = newPostText;
            await publicationPageViewModel.PublishPost();

            await postRepository.Received().PublishPost(Arg.Is<CreatePostDTO>(x => x.Message == newPostText && x.NameAndPassword == nameAndPassword));
        }
    }
}