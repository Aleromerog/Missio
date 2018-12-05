using System;
using Microsoft.EntityFrameworkCore;
using MissioServer.Services;
using NSubstitute;

namespace MissioServer.Tests
{
    public static class Utils
    {
        public static MissioContext MakeMissioContext()
        {
            var databaseOptions = new DbContextOptionsBuilder<MissioContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;
            var missioContext = new MissioContext(databaseOptions, new MockPasswordService(), Substitute.For<IWebClientService>());
            missioContext.Database.EnsureCreated();
            return missioContext;
        }
    }
}