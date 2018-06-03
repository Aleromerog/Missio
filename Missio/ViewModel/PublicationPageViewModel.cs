using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Xamarin.Forms;

namespace ViewModel
{
    public class PublicationPageViewModel
    {
        private readonly IPublishPost _publishPost;
        private readonly IGetLoggedInUser _getLoggedInUser;
        private readonly IUpdateViewPosts _updateViewPosts;
        private readonly IReturnToPreviousPage _returnOnePage;
        private string _postText;

        [UsedImplicitly]
        public string Title { get; } = "Publication page";

        [UsedImplicitly]
        public ICommand PublishPostCommand { get; }

        [UsedImplicitly]
        public string PostText
        {
            get => _postText ?? "";
            set => _postText = value;
        }

        public PublicationPageViewModel([NotNull] IPublishPost publishPost, [NotNull] IGetLoggedInUser getLoggedInUser,
            [NotNull] IUpdateViewPosts updateViewPosts, [NotNull] IReturnToPreviousPage returnOnePage)
        {
            _publishPost = publishPost ?? throw new ArgumentNullException(nameof(publishPost));
            _getLoggedInUser = getLoggedInUser ?? throw new ArgumentNullException(nameof(getLoggedInUser));
            _updateViewPosts = updateViewPosts ?? throw new ArgumentNullException(nameof(updateViewPosts));
            _returnOnePage = returnOnePage ?? throw new ArgumentNullException(nameof(returnOnePage));
            PublishPostCommand = new Command(async() => await PublishPost());
        }

        private async Task PublishPost()
        {
            _publishPost.PublishPost(new TextOnlyPost(_getLoggedInUser.LoggedInUser.UserName, PostText));
            _updateViewPosts.UpdatePosts();
            await _returnOnePage.ReturnToPreviousPage();
        }
    }
}