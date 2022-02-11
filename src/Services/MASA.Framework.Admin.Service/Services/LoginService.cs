using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Utils.Caching.DistributedMemory;
using MASA.Utils.Caching.DistributedMemory.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Caching.Memory;

namespace MASA.Framework.Admin.Service.Login.Services
{
    public class LoginService
    {
        private readonly string cacheKeyOnlineUserId = "online_user_id_";
        private readonly IMemoryCache _memoryCache;

        public LoginService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public OnlineUserModel? GetOnlineUserByUserId(int userId)
        {
            OnlineUserModel? onlineUserDTO = _memoryCache.Get<OnlineUserModel>($"{cacheKeyOnlineUserId}{userId}");

            return onlineUserDTO;
        }

        public void RemoveOnlineUserByUserId(int userId)
        {
            _memoryCache.Remove($"{cacheKeyOnlineUserId}{userId}");
        }

        public void AddOrUpdateOnlineUser(OnlineUserModel onlineUserDTO)
        {
            try
            {
                _memoryCache.Set<OnlineUserModel>($"{cacheKeyOnlineUserId}{onlineUserDTO.UserId}", onlineUserDTO);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
