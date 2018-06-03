using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.LocalProviders;
using StringResources;
using Xamarin.Forms;

namespace ViewModel
{
    public class RegistrationViewModel
    {
        private readonly IDisplayAlertOnCurrentPage _alertDisplayer;
        private readonly IRegisterUser _registerUser;
        private readonly IDoesUserExist _doesUserExist;
        private readonly IReturnToPreviousPage _returnToPreviousPage;

        [UsedImplicitly]
        public string Title { get; } = "Registration page";

        private string _userName;
        private string _password;
        private string _confirmPassword;
        private string _email;

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
        public string Email
        {
            get => _email ?? "";
            set => _email = value;
        }

        [UsedImplicitly]
        public string ConfirmPassword
        {
            get => _confirmPassword ?? "";
            set => _confirmPassword = value;
        }

        [UsedImplicitly]
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel([NotNull] IDisplayAlertOnCurrentPage alertDisplayer, [NotNull] IRegisterUser registerUser, [NotNull] IDoesUserExist doesUserExist, [NotNull] IReturnToPreviousPage returnToPreviousPage)
        {
            _alertDisplayer = alertDisplayer ?? throw new ArgumentNullException(nameof(alertDisplayer));
            _registerUser = registerUser ?? throw new ArgumentNullException(nameof(registerUser));
            _doesUserExist = doesUserExist ?? throw new ArgumentNullException(nameof(doesUserExist));
            _returnToPreviousPage = returnToPreviousPage ?? throw new ArgumentNullException(nameof(returnToPreviousPage));
            RegisterCommand = new Command(async() => await TryToRegister());
        }

        public async Task TryToRegister()
        {
            if (UserName.Length <= 3)
            {
                await _alertDisplayer.DisplayAlert(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok);
                return;
            }
            if (_doesUserExist.DoesUserExist(UserName))
            {
                await _alertDisplayer.DisplayAlert(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
                return;
            }
            if (_password != _confirmPassword)
            {
                await _alertDisplayer.DisplayAlert(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok);
                return;
            }
            if (_password.Length < 5)
            {
                await _alertDisplayer.DisplayAlert(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
                return;
            }
            _registerUser.RegisterUser(UserName, Password, Email);
            var alertTask = _alertDisplayer.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
            await  _returnToPreviousPage.ReturnToPreviousPage();
            await alertTask;
        }
    }
}