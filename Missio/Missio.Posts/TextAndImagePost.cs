using System;
using JetBrains.Annotations;
using Missio.Users;

namespace Missio.Posts
{
    public class TextAndImagePost : IPost, IMessage, IImage, IUser, IAuthorName, IUserPicture
    {
        public string Message { [UsedImplicitly] get; }
        public string Image { [UsedImplicitly] get; }
        public User UserPost { [UsedImplicitly] get; }
        public string AuthorName
        {
            [UsedImplicitly] get { return UserPost.UserName; }
        }

        public string UserPicture
        {
            [UsedImplicitly]
            get { return UserPost.Picture; }
        }

        public TextAndImagePost([NotNull] User user, [NotNull] string message, [NotNull] string image)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            UserPost = user ?? throw new ArgumentNullException(nameof(user));
        }


        public int GetPostPriority()
        {
            return 0;
        }
    }
}
