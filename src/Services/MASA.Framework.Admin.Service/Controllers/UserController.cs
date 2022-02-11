using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Framework.Admin.Service.Login.Infrastructure.Repositories;
using MASA.Framework.Admin.Service.Order.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MASA.Framework.Admin.Service.Login.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginViewModel> Login([FromServices] IUserRepository userRepository, LoginModel model)
        {
            var result = await userRepository.LoginAsync(model);

            return result;
        }

        [HttpGet]
        public async Task<UserModel> GetUser([FromServices] IUserRepository userRepository, [FromBody] int id)
        {
            var result = await userRepository.GetUserAsync(id);

            return result;
        }

        [HttpGet]
        public string Get([FromServices] IAuthRepository authRepository)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.First(x => x.Type == "UserId").Value;//从Token中拿出用户ID
                var token = authRepository.GenerateJwtToken(int.Parse(userId));
                return token;
            }
            else
            {
                return "";
            }
        }
    }
}
