using System;
using JetBrains.Annotations;
using Missio.Navigation;

namespace Missio.Registration
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