using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Framework.Admin.Service.Login.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.Login.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Login([FromServices] IUserRepository userRepository, LoginModel model)
        {
            var token = await userRepository.LoginAsync(model);
            return token;
        }

        [HttpGet]
        public async Task<UserModel> GetUser([FromServices] IUserRepository userRepository, [FromBody] int id)
        {
            var result = await userRepository.GetUserAsync(id);

            return result;
        }

        [HttpGet]
        public string Get()
        {
            return "123";
        }
    }
}
