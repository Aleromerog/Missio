using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Mission.Model.Data;
using Mission.Model.Exceptions;
using Mission.Model.LocalProviders;
using StringResources;

namespace ViewModel
{
    public class AttemptToLogIn : IAttemptToLogin
    {
        private readonly IValidateUser _userValidator;
        private readonly IDisplayAlertOnCurrentPage _alertDisplay;
        private readonly IGoToView _goToView;
        private readonly ISetLoggedInUser _setLoggedInUser;

        public AttemptToLogIn([NotNull] IValidateUser userValidator, [NotNull] IDisplayAlertOnCurrentPage alertDisplay,
            [NotNull] IGoToView goToView, [NotNull] ISetLoggedInUser setLoggedInUser)
        {
            _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
            _alertDisplay = alertDisplay ?? throw new ArgumentNullException(nameof(alertDisplay));
            _goToView = goToView ?? throw new ArgumentNullException(nameof(goToView));
            _setLoggedInUser = setLoggedInUser ?? throw new ArgumentNullException(nameof(setLoggedInUser));
        }

        public Task AttemptToLoginWithUser(User user)
        {
            try
            {
                _userValidator.ValidateUser(user);
                _setLoggedInUser.LoggedInUser = user;
                return _goToView.GoToView("News feed page");
            }
            catch (InvalidUserNameException)
            {
                return _alertDisplay.DisplayAlert(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, AppResources.Ok);
            }
            catch (InvalidPasswordException)
            {
                return _alertDisplay.DisplayAlert(AppResources.IncorrectPasswordTitle, AppResources.IncorrectPasswordMessage, AppResources.Ok);
            }
        }
    }
}