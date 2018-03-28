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

        private string userName;
        private string password;
        private string confirmPassword;
        private string email;

        [UsedImplicitly]
        public string UserName
        {
            get => userName ?? "";
            set => userName = value;
        }

        [UsedImplicitly]
        public string Password
        {
            get => password ?? "";
            set => password = value;
        }

        [UsedImplicitly]
        public string Email
        {
            get => email ?? "";
            set => email = value;
        }

        [UsedImplicitly]
        public string ConfirmPassword
        {
            get => confirmPassword ?? "";
            set => confirmPassword = value;
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

        public Task TryToRegister()
        {
            if (UserName.Length <= 3)
            {
                return _alertDisplayer.DisplayAlert(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok);
            }
            if (_doesUserExist.DoesUserExist(UserName))
            {
                return _alertDisplayer.DisplayAlert(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
            }
            if (password != confirmPassword)
            {
                return _alertDisplayer.DisplayAlert(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok);
            }
            if (password.Length < 5)
            {
                return _alertDisplayer.DisplayAlert(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
            }
            _registerUser.RegisterUser(UserName, Password, Email);
            _alertDisplayer.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
            return _returnToPreviousPage.ReturnToPreviousPage();
        }
    }
}