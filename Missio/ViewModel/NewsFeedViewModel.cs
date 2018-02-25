using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using StringResources;
using Xamarin.Forms;

namespace ViewModel
{
    public static class UserInformation
    {
        private static User loggedInUser;

        public static User LoggedInUser
        {
            get
            {
                if(loggedInUser == null)
                    throw new InvalidOperationException("No user is currently logged in");
                return loggedInUser;
            }
            set
            {
                loggedInUser = value;
                OnUserLoggedIn(null, loggedInUser);
            }
        }

        public static event EventHandler<User> OnUserLoggedIn = delegate { };  
    }

    public class NewsFeedViewModel
    {
        private readonly INewsFeedPostsProvider PostProvider;
        public ObservableCollection<NewsFeedPost> NewsFeedPosts { get; }

        public NewsFeedViewModel([NotNull] INewsFeedPostsProvider postProvider)
        {
            PostProvider = postProvider ?? throw new ArgumentNullException(nameof(postProvider));
            NewsFeedPosts = new ObservableCollection<NewsFeedPost>();
            UserInformation.OnUserLoggedIn += UserLoggedIn;
        }

        private void UserLoggedIn(object sender, User user)
        {
            NewsFeedPosts.Clear();
            foreach (var post in PostProvider.GetMostRecentPosts(user))
            {
                NewsFeedPosts.Add(post);
            }
        }
    }

    public class LogInViewModel
    {
        public Page Page { get; }
        public Page NewsFeedPage { get; }

        [UsedImplicitly]
        public string UserName
        {
            get
            {
                if (userName == null)
                    return "";
                return userName;
            }
            set => userName = value;
        }

        [UsedImplicitly]
        public string Password
        {
            get
            {
                if (password == null)
                    return "";
                return password;
            }
            set => password = value;
        }

        [UsedImplicitly]
        public ICommand LogInCommand { get; }

        private string userName;
        private string password;
        private readonly IUserValidator UserValidator;

        public LogInViewModel([NotNull] Page page, [NotNull] Page newsFeedPage, [NotNull] IUserValidator userValidator)
        {
            Page = page ?? throw new ArgumentNullException(nameof(page));
            NewsFeedPage = newsFeedPage ?? throw new ArgumentNullException(nameof(newsFeedPage));
            UserValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            LogInCommand = new Command(LogIn);
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        private async void LogIn()
        {
            var user = new User(UserName, Password);
            var attemptResult = UserValidator.IsUserValid(user);
            switch (attemptResult)
            {
                case UserValidationResult.IncorrectUsername:
                    await Page.DisplayAlert(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, AppResources.Ok);
                    break;
                case UserValidationResult.IncorrectPassword:
                    await Page.DisplayAlert(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, AppResources.Ok);
                    break;
                case UserValidationResult.Succeeded:
                    UserInformation.LoggedInUser = user;
                    await Page.Navigation.PushAsync(NewsFeedPage);
                    break;
                default:
                    throw new ArgumentException(nameof(attemptResult));
            }
        }
    }
}