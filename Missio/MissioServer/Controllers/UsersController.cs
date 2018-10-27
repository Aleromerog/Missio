using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Missio.Registration;
using Missio.Users;
using MissioServer.Services;
using MissioServer.Services.Services;
using StringResources;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MissioContext _missioContext;
        private readonly IPasswordHasher<User> _passwordService;
        private readonly IRegisterUserService _registerUserService;

        public UsersController(MissioContext missioContext, IPasswordHasher<User> passwordService, IRegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
            _passwordService = passwordService;
            _missioContext = missioContext;
        }

        [HttpGet("{name}/{password}")]
        public async Task<ActionResult<User>> GetUserIfUserValid(string name, string password)
        {
            var user = await _missioContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (user == null)
                return StatusCode(401, AppResources.InvalidUserName);
            if (_passwordService.VerifyHashedPassword(user.HashedPassword, password) == PasswordVerificationResult.Failed)
                return StatusCode(401, AppResources.InvalidPassword);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(RegistrationDTO registrationDTO)
        {
            try
            {
                await _registerUserService.RegisterUser(registrationDTO);
            }
            catch (UserRegistrationException registrationException)
            {
                return StatusCode(400, registrationException.ErrorMessages);
            }
            return Ok();
        }
    }
}
