using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
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
        public User User { get; }
        public ICommand LogInCommand { get; }
        private readonly IUserValidator UserValidator;

        public LogInViewModel(Page page, IUserValidator userValidator)
        {
            Page = page;
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
                    await Page.DisplayAlert("Incorrect username", "There does not exist a user with the given name", "Ok");
                    break;
                case LogInAttemptResult.IncorrectPassword:
                    await Page.DisplayAlert("Incorrect password", "The password was incorrect", "Ok");
                    break;
                case LogInAttemptResult.Succeeded:
                    await Page.DisplayAlert("Login succeeded", ":D", "Ok");
                    break;
                default:
                    throw new ArgumentException(nameof(attemptResult));
            }
        }
    }
}