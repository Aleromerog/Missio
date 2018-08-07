using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.LocalDatabase;
using Missio.Users;
using StringResources;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.Registration
{
    public class RegistrationViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly INavigation _navigation;
        private string _userName;
        private string _password;
        private string _email;

        public string UserName
        {
            get => _userName ?? "";
            set => _userName = value;
        }

        public string Password
        {
            get => _password ?? "";
            set => _password = value;
        }

        public string Email
        {
            get => _email ?? "";
            set => _email = value;
        }

        [UsedImplicitly]
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel([NotNull] IUserRepository registerUser, [NotNull] INavigation navigation)
        {
            _userRepository = registerUser ?? throw new ArgumentNullException(nameof(registerUser));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            RegisterCommand = new Command(async() => await TryToRegister());
        }

        public async Task TryToRegister()
        {
            try
            {
                await SendRegistrationAndResetView();
            }
            catch(UserRegistrationException registrationException)
            {
                Console.WriteLine(registrationException.ErrorMessages[0].Message);
                await _navigation.DisplayAlert(registrationException.ErrorMessages[0]);
            }
        }

        private async Task SendRegistrationAndResetView()
        {
            _userRepository.AttemptToRegisterUser(new User(UserName, Password, Email));
            var alertTask = _navigation.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
            await _navigation.ReturnToPreviousPage();
            await alertTask;
        }
    }
}