using MASA.Framework.Admin.Contracts.Login.Model;
using Microsoft.Extensions.Caching.Memory;

namespace MASA.Framework.Admin.Service.Api.Services
{
    public class LoginService
    {
        private readonly string cacheKeyOnlineUserId = "online_user_id";
        private readonly IMemoryCache _memoryCache;

        public LoginService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public OnlineUserModel? GetOnlineUserByUserId(int userId)
        {
            //OnlineUserModel? onlineUserDTO = _memoryCache.Get<OnlineUserModel>($"{cacheKeyOnlineUserId}{userId}");
            var users = _memoryCache.Get<List<OnlineUserModel>>(cacheKeyOnlineUserId);
            var user = users?.FirstOrDefault(u => u.UserId == userId);

            return user;
        }

        public void RemoveOnlineUserByUserId(int userId)
        {
            _memoryCache.Remove($"{cacheKeyOnlineUserId}{userId}");
        }

        public void AddOrUpdateOnlineUsers(List<OnlineUserModel> onlineUsers)
        {
            _memoryCache.Set($"{cacheKeyOnlineUserId}", onlineUsers);
        }
    }
}
