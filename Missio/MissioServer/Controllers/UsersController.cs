using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Missio.Registration;
using Missio.Users;
using MissioServer.Services.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IRegisterUserService registerUserService)
        {
            _userService = userService;
            _registerUserService = registerUserService;
        }

        public async Task<ActionResult> ValidateUser(string userName, string password)
        {
            await _userService.GetUserIfValid(userName, password);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser(CreateUserDTO createUserDTO)
        {
            try
            {
                await _registerUserService.RegisterUser(createUserDTO);
            }
            catch (UserRegistrationException registrationException)
            {
                return StatusCode(400, registrationException.ErrorMessages);
            }
            return Ok();
        }
    }
}
