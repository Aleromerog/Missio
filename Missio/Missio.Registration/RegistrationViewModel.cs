using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Missio.Navigation;
using StringResources;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace Missio.Registration
{
    public class RegistrationViewModel
    {
        private readonly IDisplayAlertOnCurrentPage _alertDisplayer;
        private readonly IRegisterUser _registerUser;
        private readonly INavigation _navigation;

        public RegistrationInfo RegistrationInfo { get; }

        [UsedImplicitly]
        public ICommand RegisterCommand { get; set; }

        public RegistrationViewModel([NotNull] IDisplayAlertOnCurrentPage alertDisplayer, [NotNull] IRegisterUser registerUser, [NotNull] INavigation navigation)
        {
            _alertDisplayer = alertDisplayer ?? throw new ArgumentNullException(nameof(alertDisplayer));
            _registerUser = registerUser ?? throw new ArgumentNullException(nameof(registerUser));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            RegistrationInfo = new RegistrationInfo("", "", "");
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
            await _navigation.ReturnToPreviousPage();
            await alertTask;
        }
    }
}