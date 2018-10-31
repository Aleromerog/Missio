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
        public async Task<User> GetUserIfValid(string userName, string password)
        {
            var user = await _missioContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
                throw new InvalidUserNameException();
            if (_passwordService.VerifyHashedPassword(user.HashedPassword, password) == PasswordVerificationResult.Failed)
                throw new InvalidPasswordException();
            return user;
        }

        /// <inheritdoc />
        public Task<UserFriends> GetFriends(User user, User requester)
        {
            return _missioContext.UsersFriends.Include(x => x.Friends).FirstAsync(x => x.User == user);
        }
    }
}