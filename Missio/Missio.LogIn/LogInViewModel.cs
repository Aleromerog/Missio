using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.Navigation;
using Missio.Users;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.LogIn
{
    public class LogInViewModel<TRegistrationPage> where TRegistrationPage : Page
    {
        public User User { get; set; }

        [UsedImplicitly]
        public string UserName
        {
            get => User.UserName ?? "";
            set => User = value == null ? new User("", User.Password) : new User(value, User.Password);
        }

        [UsedImplicitly]
        public string Password
        {
            get => User.Password ?? "";
            set => User = value == null ? new User(User.UserName, "") : new User(User.UserName, value);
        }

        [UsedImplicitly]
        public ICommand LogInCommand { get; }

        [UsedImplicitly]
        public ICommand GoToRegistrationPageCommand { get; }

        private readonly IValidateUser _userValidator;
        private readonly IDisplayAlertOnCurrentPage _alertDisplay;
        private readonly INavigation _navigation;
        private readonly ISetLoggedInUser _setLoggedInUser;

        public LogInViewModel([NotNull] INavigation navigation, [NotNull] IValidateUser userValidator,
            [NotNull] IDisplayAlertOnCurrentPage alertDisplay, [NotNull] ISetLoggedInUser setLoggedInUser)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            _alertDisplay = alertDisplay ?? throw new ArgumentNullException(nameof(alertDisplay));
            _setLoggedInUser = setLoggedInUser ?? throw new ArgumentNullException(nameof(setLoggedInUser));
            User = new User("", "");
            GoToRegistrationPageCommand = new Command(() => navigation.GoToPage<TRegistrationPage>());
            LogInCommand = new Command(async() => await LogIn());
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        private async Task LogIn()
        {
            try
            {
                _userValidator.ValidateUser(User);
                _setLoggedInUser.LoggedInUser = User;
                await _navigation.GoToPage<MainTabbedPage>();
            }
            catch (LogInException e)
            {
                await _alertDisplay.DisplayAlert(e.AlertTextMessage);
            }
        }
    }
}