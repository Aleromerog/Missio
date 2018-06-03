using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Data;
using Xamarin.Forms;

namespace ViewModel
{
    public class LogInViewModel
    {
        [UsedImplicitly]
        public string UserName
        {
            get
            {
                if (_userName == null)
                    return "";
                return _userName;
            }
            set => _userName = value;
        }

        [UsedImplicitly]
        public string Password
        {
            get
            {
                if (_password == null)
                    return "";
                return _password;
            }
            set => _password = value;
        }

        [UsedImplicitly]
        public ICommand LogInCommand { get; }

        [UsedImplicitly]
        public ICommand GoToRegistrationPageCommand { get; }

        private string _userName;
        private string _password;
        private readonly IAttemptToLogin _loginAttempt;

        public LogInViewModel([NotNull] IAttemptToLogin loginAttempt, [NotNull] IGoToView goToView)
        {
            if (goToView == null)
                throw new ArgumentNullException(nameof(goToView));
            GoToRegistrationPageCommand = new Command(() => goToView.GoToView("Registration page"));
            _loginAttempt = loginAttempt ?? throw new ArgumentNullException(nameof(loginAttempt));
            LogInCommand = new Command(async() => await LogIn());
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        private async Task LogIn()
        {
            var user = new User(UserName, Password);
            await _loginAttempt.AttemptToLoginWithUser(user);
        }
    }
}