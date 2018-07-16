using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.LogIn;
using Missio.NewsFeed;
using Missio.Posts;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.PostPublication
{
    public class PublicationPageViewModel
    {
        private readonly IPublishPost _publishPost;
        private readonly IGetLoggedInUser _getLoggedInUser;
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

        public PublicationPageViewModel([NotNull] IPublishPost publishPost, [NotNull] IGetLoggedInUser getLoggedInUser,
            [NotNull] IUpdateViewPosts updateViewPosts, [NotNull] INavigation navigation)
        {
            _publishPost = publishPost ?? throw new ArgumentNullException(nameof(publishPost));
            _getLoggedInUser = getLoggedInUser ?? throw new ArgumentNullException(nameof(getLoggedInUser));
            _updateViewPosts = updateViewPosts ?? throw new ArgumentNullException(nameof(updateViewPosts));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            PublishPostCommand = new Command(async() => await PublishPost());
        }

        private async Task PublishPost()
        {
            _publishPost.PublishPost(new TextOnlyPost(_getLoggedInUser.LoggedInUser.UserName, PostText));
            _updateViewPosts.UpdatePosts();
            await _navigation.ReturnToPreviousPage();
        }
    }
}