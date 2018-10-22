using System;
using System.Collections.Generic;
using Missio.Navigation;

namespace Missio.LocalDatabase
{
    public class UserRegistrationException : Exception
    {
        public readonly List<AlertTextMessage> ErrorMessages;

        public UserRegistrationException(List<AlertTextMessage> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}