using JetBrains.Annotations;

namespace Missio.Posts
{
    public class CreatePostDTO
    {
        [UsedImplicitly]
        public string UserName { get; set; }

        [UsedImplicitly]
        public string Password { get; set; }

        [UsedImplicitly]
        public string Message { get; set; }

        [UsedImplicitly]
        public byte[] Picture { get; set; }

        [UsedImplicitly]
        public CreatePostDTO()
        {
        }

        public CreatePostDTO(string userName, string password, string message, byte[] picture)
        {
            UserName = userName;
            Password = password;
            Message = message;
            Picture = picture;
        }
    }
}