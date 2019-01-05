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
        private readonly UsersService _userService;

        public UsersController(UsersService userService)
        {
            _userService = userService;
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
                await _userService.RegisterUser(createUserDTO);
            }
            catch (UserRegistrationException registrationException)
            {
                return StatusCode(400, registrationException.ErrorMessages);
            }
            return Ok();
        }
    }
}
