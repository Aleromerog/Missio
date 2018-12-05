using System.Threading.Tasks;
using Domain;
using Domain.DataTransferObjects;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MissioServer.Services;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IRegisterUserService registerUserService)
        {
            _userService = userService;
            _registerUserService = registerUserService;
        }

        [HttpGet("{userName}/{password}")]
        public async Task<ActionResult> ValidateUser(string userName, string password)
        {
            await _userService.GetUserIfValid(new NameAndPassword(userName, password));
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
