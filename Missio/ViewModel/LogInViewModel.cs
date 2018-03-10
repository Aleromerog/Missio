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
        private readonly IAttemptToLogin _loginAttempt;

        public LogInViewModel(IAttemptToLogin loginAttempt)
        {
            _loginAttempt = loginAttempt;
            LogInCommand = new Command(LogIn);
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        public async void LogIn()
        {
            var user = new User(UserName, Password);
            await _loginAttempt.AttemptToLoginWithUser(user);
        }
    }
}