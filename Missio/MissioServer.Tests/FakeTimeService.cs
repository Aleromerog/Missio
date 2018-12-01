using System;
using MissioServer.Services;

namespace MissioServer.Tests
{
    public class FakeTimeService : ITimeService
    {
        /// <inheritdoc />
        public DateTime GetCurrentTime()
        {
            return new DateTime(2018, 9, 2);
        }
    }
}