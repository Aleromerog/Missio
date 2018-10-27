using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Missio.Registration;
using Missio.Users;
using StringResources;

namespace MissioServer.Services.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly MissioContext _missioContext;
        private readonly IPasswordHasher<User> _passwordService;

        public RegisterUserService(MissioContext missioContext, IPasswordHasher<User> passwordService)
        {
            _passwordService = passwordService;
            _missioContext = missioContext;
        }

        /// <inheritdoc />
        public async Task RegisterUser(RegistrationDTO registration)
        {
            var errors = new List<string>();
            var userName = registration.UserName;
            var password = registration.Password;
            var email = registration.Email;
            if(userName.Length < 5)
                errors.Add(AppResources.UserNameTooShortMessage);
            if(password.Length < 4)
                errors.Add(AppResources.PasswordTooShortMessage);
            if(await _missioContext.Users.AnyAsync(x => x.UserName == userName))
                errors.Add(AppResources.UserNameAlreadyInUseMessage);
            if(errors.Count > 0)
                throw new UserRegistrationException(errors);
            _missioContext.Add(new User(userName, _passwordService.HashPassword(password), null, email));
            _missioContext.SaveChanges();
        }
    }
}