using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.Posts;
using Missio.Users;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.PostPublication
{
    public class PublicationPageViewModel
    {
        private readonly IPostRepository _postRepository;
        private readonly ILoggedInUser _loggedInUser;
        private readonly IUpdateViewPosts _updateViewPosts;
        private readonly INavigation _navigation;
        private string _postText;
        
        [UsedImplicitly]
        public ICommand PublishPostCommand { get; }

        [UsedImplicitly]
        public string PostText
        {
            get => _postText ?? "";
            set => _postText = value;
        }

        public PublicationPageViewModel([NotNull] IPostRepository postRepository, [NotNull] ILoggedInUser loggedInUser,
            [NotNull] IUpdateViewPosts updateViewPosts, [NotNull] INavigation navigation)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _loggedInUser = loggedInUser ?? throw new ArgumentNullException(nameof(loggedInUser));
            _updateViewPosts = updateViewPosts ?? throw new ArgumentNullException(nameof(updateViewPosts));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            PublishPostCommand = new Command(async() => await PublishPost());
        }

        public async Task PublishPost()
        {
            _postRepository.PublishPost(new Post(_loggedInUser.LoggedInUser, PostText));
            _updateViewPosts.UpdatePosts();
            await _navigation.ReturnToPreviousPage();
        }
    }
}