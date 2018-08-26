using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Missio.Users;

namespace Missio.Posts
{
    public class TextAndImagePost : IPost, IMessage, IImage, IUser, IAuthorName, IUserPicture
    {
        public string Message { [UsedImplicitly] get; }
        public string Image { [UsedImplicitly] get; }
        public User UserPost { [UsedImplicitly] get; }
        public string AuthorName {[UsedImplicitly] get; }
        public string UserPicture {[UsedImplicitly] get; }

        public TextAndImagePost([NotNull] User user, [NotNull] string message, [NotNull] string image)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            UserPost = user ?? throw new ArgumentNullException(nameof(user));
            AuthorName = UserPost.UserName ?? throw new ArgumentNullException(nameof(user.UserName));
            UserPicture = UserPost.Picture ?? throw new ArgumentNullException(nameof(user.Picture));
        }


        public int GetPostPriority()
        {
            return 0;
        }
    }
}
