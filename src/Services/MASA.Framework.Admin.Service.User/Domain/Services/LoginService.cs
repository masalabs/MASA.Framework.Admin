using Masa.Framework.Admin.Service.User.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Masa.Framework.Admin.Service.User.Domain.Services
{
    public class LoginService
    {
        readonly string cacheKeyOnlineUserId = "online_user_id";
        readonly IMemoryCache _memoryCache;

        public LoginService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public OnlineUserModel? GetOnlineUserByUserId(Guid userId)
        {
            var users = _memoryCache.Get<List<OnlineUserModel>>(cacheKeyOnlineUserId);
            var user = users?.FirstOrDefault(u => u.UserId == userId);

            return user;
        }

        public void RemoveOnlineUserByUserId(Guid userId)
        {
            _memoryCache.Remove($"{cacheKeyOnlineUserId}{userId}");
        }

        public void AddOrUpdateOnlineUsers(List<OnlineUserModel> onlineUsers)
        {
            _memoryCache.Set($"{cacheKeyOnlineUserId}", onlineUsers);
        }

        public string GenerateJwtToken(Guid userId, string security, int expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(security);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
