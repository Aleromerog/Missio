using JetBrains.Annotations;

namespace Missio.Users
{
    public class CreateUserDTO
    {
        [UsedImplicitly]
        public string UserName { get; set; }

        [UsedImplicitly]
        public string Password { get; set; }

        [UsedImplicitly]
        public string Email { get; set; }

        [UsedImplicitly]
        public CreateUserDTO()
        {
        }

        public CreateUserDTO(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}