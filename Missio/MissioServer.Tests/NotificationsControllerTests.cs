using System.Threading.Tasks;
using MissioServer.Controllers;
using MissioServer.Services;
using NSubstitute;
using NUnit.Framework;

namespace MissioServer.Tests
{
    [TestFixture]
    public class NotificationsControllerTests
    {
        private NotificationsController MakeNotificationsController()
        {
            var missioContext = Utils.MakeMissioContext();
            var usersService = new UsersService(missioContext, new MockPasswordService(), Substitute.For<IWebClientService>());
            return new NotificationsController(usersService, new NotificationsService(missioContext));
        }

        [Test]
        public async Task GetPostLikedNotifications_Should_ReturnNotifications()
        {
            var notificationsController = MakeNotificationsController();

            var result = await notificationsController.GetPostLikedNotifications("Francisco Greco", "ElPass");

            Assert.AreEqual(1, result.Count);
        }
    }
}