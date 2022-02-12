using MASA.Framework.Admin.Contracts.Login.Model;
using MASA.Framework.Admin.Service.Api.Infrastructure.Repositories;
using MASA.Framework.Admin.Service.Order.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MASA.Framework.Admin.Service.Login.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public UserController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

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
        public int GetOnlineUserCount()
        {
            var users = _memoryCache.Get<List<OnlineUserModel>>("online_user_id");
            return users?.Count ?? 0;
        }
    }
}
