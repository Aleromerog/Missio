using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Missio.Users;

namespace MissioServer.Services.Services
{
    public class UsersService : IUserService
    {
        private readonly MissioContext _missioContext;
        private readonly IPasswordHasher<User> _passwordService;

        public UsersService(MissioContext missioContext, IPasswordHasher<User> passwordService)
        {
            _passwordService = passwordService;
            _missioContext = missioContext;
        }
        /// <inheritdoc />
        public async Task<User> GetUserIfValid(NameAndPassword nameAndPassword)
        {
            var user = await _missioContext.Users.FirstOrDefaultAsync(x => x.UserName == nameAndPassword.UserName);
            if (user == null)
                throw new InvalidUserNameException();
            var credentials = await _missioContext.UsersCredentials.FirstAsync(x => x.User == user);
            if (_passwordService.VerifyHashedPassword(credentials.HashedPassword, nameAndPassword.Password) == PasswordVerificationResult.Failed)
                throw new InvalidPasswordException();
            return user;
        }

        /// <inheritdoc />
        public Task<UserFriends> GetFriends(User user)
        {
            return _missioContext.UsersFriends.Include(x => x.Friends).FirstAsync(x => x.User == user);
        }
    }
}