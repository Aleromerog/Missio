using System;
using JetBrains.Annotations;
using Mission.Model.Services;

namespace Mission.Model.Exceptions
{
    public class RegistrationException  : Exception
    {
        public readonly AlertTextMessage AlertMessage;

        public RegistrationException([NotNull] AlertTextMessage alertMessage)
        {
            AlertMessage = alertMessage ?? throw new ArgumentNullException(nameof(alertMessage));
        }
    }
}