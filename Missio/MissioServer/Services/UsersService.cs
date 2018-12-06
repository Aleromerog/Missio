﻿using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MissioServer.Services
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
        public IQueryable<User> GetFriends(User user)
        {
            var userFriendships = _missioContext.Friendships.Where(x => x.User == user);
            return userFriendships.Select(x => x.Friend);
        }
    }
}