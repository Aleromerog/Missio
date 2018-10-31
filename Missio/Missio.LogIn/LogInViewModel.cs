using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.MainView;
using Missio.Registration;
using Missio.Users;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.LogIn
{
    public class LogInViewModel
    {
        private string _userName;
        private string _password;

        [UsedImplicitly]
        public string UserName
        {
            get => _userName ?? "";
            set => _userName = value;
        }

        [UsedImplicitly]
        public string Password
        {
            get => _password ?? "";
            set => _password = value;
        }

        [UsedImplicitly]
        public ICommand LogInCommand { get; }

        [UsedImplicitly]
        public ICommand GoToRegistrationPageCommand { get; }

        private readonly IUserRepository _userRepository;
        private readonly INavigation _navigation;
        private readonly ILoggedInUser _loggedInUser;

        public LogInViewModel([NotNull] INavigation navigation, [NotNull] IUserRepository userRepository, [NotNull] ILoggedInUser loggedInUser)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _loggedInUser = loggedInUser ?? throw new ArgumentNullException(nameof(loggedInUser));
            GoToRegistrationPageCommand = new Command(() => navigation.GoToPage<RegistrationPage>());
            LogInCommand = new Command(async() => await LogIn());
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        public async Task LogIn()
        {
            try
            {
                await _userRepository.ValidateUser(UserName, Password);
                _loggedInUser.UserName = UserName;
                _loggedInUser.Password = Password;
                await _navigation.GoToPage<MainTabbedPage>();
            }
            catch (LogInException e)
            {
                await _navigation.DisplayAlert(e.AlertTextMessage);
            }
        }
    }
}