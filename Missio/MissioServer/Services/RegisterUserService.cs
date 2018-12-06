using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Missio.ApplicationResources;

namespace MissioServer.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly MissioContext _missioContext;
        private readonly IPasswordHasher<User> _passwordService;
        private readonly IWebClientService _webClientService;

        public RegisterUserService(MissioContext missioContext, IPasswordHasher<User> passwordService, IWebClientService webClientService)
        {
            _passwordService = passwordService;
            _webClientService = webClientService;
            _missioContext = missioContext;
        }

        /// <inheritdoc />
        public async Task RegisterUser(CreateUserDTO createUserDTO)
        {
            var userName = createUserDTO.UserName;
            var password = createUserDTO.Password;
            var email = createUserDTO.Email;
            var picture = createUserDTO.Picture ?? _webClientService.DownloadData("https://upload.wikimedia.org/wikipedia/commons/thumb/9/93/Default_profile_picture_%28male%29_on_Facebook.jpg/600px-Default_profile_picture_%28male%29_on_Facebook.jpg");
            var errors = new List<string>();

            if (userName.Length < 5)
                errors.Add(Strings.UserNameTooShortMessage);
            if(password.Length < 4)
                errors.Add(Strings.PasswordTooShortMessage);
            if(await _missioContext.Users.AnyAsync(x => x.UserName == userName))
                errors.Add(Strings.UserNameAlreadyInUseMessage);
            if(errors.Count > 0)
                throw new UserRegistrationException(errors);
            var newUser = new User(userName, picture, email);
            _missioContext.Add(newUser);
            _missioContext.Add(new UserCredentials(newUser, _passwordService.HashPassword(password)));
            _missioContext.SaveChanges();
        }
    }
}