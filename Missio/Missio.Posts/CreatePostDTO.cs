using JetBrains.Annotations;

namespace Missio.Posts
{
    public class CreatePostDTO
    {
        public string UserName { get; }
        public string Password { get; }
        public string Message { get; }
        public byte[] Picture { get; }

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