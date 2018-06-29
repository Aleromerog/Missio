using Missio.Navigation;
using StringResources;

namespace Missio.LogIn
{
    public class InvalidUserNameException : LogInException
    {
        /// <inheritdoc />
        public InvalidUserNameException() : base(new AlertTextMessage(AppResources.IncorrectUserNameTitle, AppResources.IncorrectUserNameMessage, AppResources.Ok))
        {
        }
    }
}