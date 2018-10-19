using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Missio.Users;
using StringResources;

namespace MissioServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MissioContext _missioContext;

        public UsersController(MissioContext missioContext)
        {
            _missioContext = missioContext;
        }

        [HttpGet("{name}&{password}")]
        public async Task<ActionResult<User>> IsUserValid(string name, string password)
        {
            var user = await _missioContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (user == null)
                return StatusCode(401, AppResources.InvalidUserName);
            if (user.Password != password)
                return StatusCode(401, AppResources.InvalidPassword);
            return user;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
