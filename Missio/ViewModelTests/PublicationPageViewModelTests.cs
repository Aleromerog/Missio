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
        private PublicationPageViewModel MakePublicationPageViewModel(IPostRepository postRepository, INavigation navigation, INameAndPasswordService nameAndPasswordService = null)
        {
            if (nameAndPasswordService == null)
            {
                nameAndPasswordService = Substitute.For<INameAndPasswordService>();
                nameAndPasswordService.NameAndPassword.Returns(new NameAndPassword("", ""));
            }

            return new PublicationPageViewModel(postRepository, navigation, nameAndPasswordService);
        }

        [Test]
        public async Task PublishPost_TextOnly_FiresCallback()
        {
            var wasActionCalled = false;
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), Substitute.For<INavigation>());
            publicationPageViewModel.PostPublishedEvent += (s, e) => { wasActionCalled = true; };

            await publicationPageViewModel.PublishPost();

            Assert.IsTrue(wasActionCalled);
        }

        [Test]
        public async Task PublishPost_TextOnly_ReturnsToNewsFeed()
        {
            var navigation = Substitute.For<INavigation>();
            var publicationPageViewModel = MakePublicationPageViewModel(Substitute.For<IPostRepository>(), navigation);

            await publicationPageViewModel.PublishPost();

            await navigation.Received(1).ReturnToPreviousPage();
        }
        
        [Test]
        public async Task PublishPostCommand_TextOnly_PublishesPost()
        {
            var nameAndPasswordService = Substitute.For<INameAndPasswordService>();
            var nameAndPassword = new NameAndPassword("Francisco Greco", "ElPass");
            nameAndPasswordService.NameAndPassword.Returns(nameAndPassword);
            var newPostText = "The content of the new post";
            var postRepository = Substitute.For<IPostRepository>();
            var publicationPageViewModel = MakePublicationPageViewModel(postRepository, Substitute.For<INavigation>(), nameAndPasswordService);
            publicationPageViewModel.PostText = newPostText;
            await publicationPageViewModel.PublishPost();

            await postRepository.Received().PublishPost(Arg.Is<CreatePostDTO>(x => x.Message == newPostText && x.NameAndPassword == nameAndPassword));
        }
    }
}