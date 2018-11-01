using System;

namespace MissioServer.Services.Services
{
    public class TimeService : ITimeService
    {
        /// <inheritdoc />
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}