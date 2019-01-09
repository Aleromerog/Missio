using System.Linq;
using Domain;

namespace MissioServer.Services
{
    public class NotificationsService
    {
        private readonly MissioContext _missioContext;

        public NotificationsService(MissioContext missioContext)
        {
            _missioContext = missioContext;
        }

        public IQueryable<PostLikedNotification> GetPostLikedNotifications(User user)
        {
            return _missioContext.PostLikedNotifications.Where(x => x.UserToBeNotified == user);
        }

        public void NotifyPostLiked(Post post, User userThatLikedThePost)
        {
            var notification = new PostLikedNotification(userThatLikedThePost, post.Author, post);
            _missioContext.PostLikedNotifications.Add(notification);
            _missioContext.SaveChanges();
        }
    }
}