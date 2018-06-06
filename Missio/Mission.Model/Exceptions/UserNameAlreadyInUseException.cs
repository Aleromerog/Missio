using Mission.Model.Services;
using StringResources;

namespace Mission.Model.Exceptions
{
    public class UserNameAlreadyInUseException : RegistrationException
    {
        /// <inheritdoc />
        public UserNameAlreadyInUseException() : base(new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok))
        {
        }
    }
}