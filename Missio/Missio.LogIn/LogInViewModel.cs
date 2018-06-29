using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.Navigation;
using Missio.Users;
using Xamarin.Forms;

namespace Missio.LogIn
{
    public class LogInViewModel
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
        private readonly IGoToView _goToView;
        private readonly ISetLoggedInUser _setLoggedInUser;

        public LogInViewModel([NotNull] IGoToView goToView, [NotNull] IValidateUser userValidator,
            [NotNull] IDisplayAlertOnCurrentPage alertDisplay, [NotNull] ISetLoggedInUser setLoggedInUser)
        {
            _goToView = goToView ?? throw new ArgumentNullException(nameof(goToView));
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            _alertDisplay = alertDisplay ?? throw new ArgumentNullException(nameof(alertDisplay));
            _setLoggedInUser = setLoggedInUser ?? throw new ArgumentNullException(nameof(setLoggedInUser));
            User = new User("", "");
            GoToRegistrationPageCommand = new Command(() => goToView.GoToView("Registration page"));
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
                await _goToView.GoToView("Main tabbed page");
            }
            catch (LogInException e)
            {
                await _alertDisplay.DisplayAlert(e.AlertTextMessage);
            }
        }
    }
}