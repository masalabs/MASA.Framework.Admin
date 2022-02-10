using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Utils.Caching.DistributedMemory;
using MASA.Utils.Caching.DistributedMemory.Interfaces;
using Microsoft.AspNetCore.Components;

namespace MASA.Framework.Admin.Web.Services
{
    public class LoginService
    {
        private readonly string cacheKeyOnlineUserId = "online_user_id_";
        private readonly MemoryCacheClient _memoryCacheClient;

        public LoginService(IMemoryCacheClientFactory MemoryCacheClientFactory)
        {
            _memoryCacheClient = MemoryCacheClientFactory.CreateClient("masa.hks");
        }

        public async Task<OnlineUserModel> GetOnlineUserByUserIdAsync(int userId)
        {
            OnlineUserModel onlineUserDTO = await _memoryCacheClient.GetAsync<OnlineUserModel>($"{cacheKeyOnlineUserId}{userId}") ?? new OnlineUserModel();

            return onlineUserDTO;
        }

        public async Task RemoveOnlineUserByUserIdAsync(int userId)
        {
            await _memoryCacheClient.RemoveAsync<OnlineUserModel>($"{cacheKeyOnlineUserId}{userId}");
        }

        public async Task AddOrUpdateOnlineUserAsync(OnlineUserModel onlineUserDTO)
        {
            await _memoryCacheClient.SetAsync<OnlineUserModel>($"{cacheKeyOnlineUserId}{onlineUserDTO.UserId}", onlineUserDTO);
        }
    }
}
