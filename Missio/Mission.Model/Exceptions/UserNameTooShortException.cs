using Mission.Model.Services;
using StringResources;

namespace Mission.Model.Exceptions
{
    public class UserNameTooShortException : RegistrationException
    {
        /// <inheritdoc />
        public UserNameTooShortException() : base(new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok))
        {
        }
    }
}