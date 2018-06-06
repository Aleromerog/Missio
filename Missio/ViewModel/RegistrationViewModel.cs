using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Mission.Model.Exceptions;
using Mission.Model.Services;
using StringResources;
using Xamarin.Forms;

namespace ViewModel
{
    public class RegistrationViewModel
    {
        private readonly IDisplayAlertOnCurrentPage _alertDisplayer;
        private readonly IRegisterUser _registerUser;
        private readonly IReturnToPreviousPage _returnToPreviousPage;

        [UsedImplicitly]
        public string Title { get; } = "Registration page";

        public readonly RegistrationInfo RegistrationInfo;

        [UsedImplicitly]
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel([NotNull] IDisplayAlertOnCurrentPage alertDisplayer, [NotNull] IRegisterUser registerUser, [NotNull] IReturnToPreviousPage returnToPreviousPage)
        {
            _alertDisplayer = alertDisplayer ?? throw new ArgumentNullException(nameof(alertDisplayer));
            _registerUser = registerUser ?? throw new ArgumentNullException(nameof(registerUser));
            _returnToPreviousPage = returnToPreviousPage ?? throw new ArgumentNullException(nameof(returnToPreviousPage));
            RegistrationInfo = new RegistrationInfo("", "", "", "");
            RegisterCommand = new Command(async() => await TryToRegister());
        }

        public async Task TryToRegister()
        {
            try
            {
                await SendRegistrationAndResetView();
            }
            catch(RegistrationException registrationException)
            {
                await _alertDisplayer.DisplayAlert(registrationException.AlertMessage);
            }
        }

        private async Task SendRegistrationAndResetView()
        {
            _registerUser.RegisterUser(RegistrationInfo);
            var alertTask = _alertDisplayer.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
            await _returnToPreviousPage.ReturnToPreviousPage();
            await alertTask;
        }
    }
}