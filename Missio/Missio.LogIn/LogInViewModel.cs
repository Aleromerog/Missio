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

        public LogInViewModel([NotNull] INavigation navigation, [NotNull] IUserRepository userRepository)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
                var nameAndPassword = new NameAndPassword(UserName, Password);
                await _userRepository.ValidateUser(nameAndPassword);
                await _navigation.GoToPage<MainTabbedPage>(nameAndPassword);
            }
            catch (LogInException e)
            {
                await _navigation.DisplayAlert(e.AlertTextMessage);
            }
        }
    }
}