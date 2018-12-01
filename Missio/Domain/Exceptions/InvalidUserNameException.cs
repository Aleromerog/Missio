using Missio.ApplicationResources;

namespace Domain.Exceptions
{
    public class InvalidUserNameException : LogInException
    {
        /// <inheritdoc />
        public InvalidUserNameException() : base(Strings.InvalidUserName)
        {
        }
    }
}