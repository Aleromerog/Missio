using System;
using System.Collections.Generic;

namespace Domain.Exceptions
{
    public class UserRegistrationException : Exception
    {
        public readonly List<string> ErrorMessages;

        public UserRegistrationException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}