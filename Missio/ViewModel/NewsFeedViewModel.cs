using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using StringResources;
using Xamarin.Forms;

namespace ViewModel
{
    public class NewsFeedViewModel
    {
        private readonly INewsFeedPositionProvider PostProvider;
        public ObservableCollection<NewsFeedPost> NewsFeedPosts { get; }

        public NewsFeedViewModel(INewsFeedPositionProvider postProvider)
        {
            PostProvider = postProvider;
            NewsFeedPosts = PostProvider.GetMostRecentPosts();
        }
    }

    public class LogInViewModel
    {
        public Page Page { get; }
        public Page NewsFeedPage { get; }
        public User User { get; }
        public ICommand LogInCommand { get; }
        private readonly IUserValidator UserValidator;

        public LogInViewModel(Page page, Page newsFeedPage, IUserValidator userValidator)
        {
            Page = page;
            NewsFeedPage = newsFeedPage;
            UserValidator = userValidator;
            LogInCommand = new Command(LogIn);
            User = new User("", "");
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        private async void LogIn()
        {
            var attemptResult = UserValidator.IsDataCorrect(User);
            switch (attemptResult)
            {
                case LogInAttemptResult.IncorrectUsername:
                    await Page.DisplayAlert(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, "Ok");
                    break;
                case LogInAttemptResult.IncorrectPassword:
                    await Page.DisplayAlert(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, "Ok");
                    break;
                case LogInAttemptResult.Succeeded:
                    await Page.Navigation.PushAsync(NewsFeedPage);
                    break;
                default:
                    throw new ArgumentException(nameof(attemptResult));
            }
        }
    }
}