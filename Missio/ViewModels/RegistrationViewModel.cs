using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.DataTransferObjects;
using Domain.Exceptions;
using Domain.Repositories;
using JetBrains.Annotations;
using Missio.ApplicationResources;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModels
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

        public RegistrationViewModel([NotNull] IUserRepository userRepository, [NotNull] INavigation navigation)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
                await _navigation.DisplayAlert(Strings.TheRegistrationFailed, String.Join(Environment.NewLine,  registrationException.ErrorMessages), Strings.Ok);
            }
        }

        private async Task SendRegistrationAndResetView()
        {
            var registration = new CreateUserDTO(UserName, Password, Email);
            await _userRepository.AttemptToRegisterUser(registration);
            var alertTask = _navigation.DisplayAlert(Strings.RegistrationSuccessfulTitle, Strings.RegistrationSuccessfulMessage, Strings.Ok);
            await _navigation.ReturnToPreviousPage();
            await alertTask;
        }
    }
}