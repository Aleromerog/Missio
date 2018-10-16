using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Missio.LogIn;

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
        public async Task<LogInStatus> IsUserValid(string name, string password)
        {
            var user = await _missioContext.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (user == null)
                return LogInStatus.InvalidUserName;
            if (user.Password != password)
                return LogInStatus.InvalidPassword;
            return LogInStatus.Successful;
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
