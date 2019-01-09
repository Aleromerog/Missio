using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MissioServer.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsController
    {
        private readonly UsersService _userService;
        private readonly NotificationsService _notificationsService;

        public NotificationsController(UsersService userService, NotificationsService notificationsService)
        {
            _userService = userService;
            _notificationsService = notificationsService;
        }

        [HttpGet("getPostLikedNotifications/{userName}/{password}")]
        public async Task<List<PostLikedNotification>> GetPostLikedNotifications(string userName, string password)
        {
            var user = await _userService.GetUserIfValid(new NameAndPassword(userName, password));
            return _notificationsService.GetPostLikedNotifications(user).ToList();
        }
    }
}