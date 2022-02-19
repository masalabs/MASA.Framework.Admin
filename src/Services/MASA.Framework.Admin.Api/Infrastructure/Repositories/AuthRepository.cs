using MASA.Framework.Admin.Contracts.Login.Model;
using MASA.Utils.Configuration.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MASA.Framework.Admin.Service.Order.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public Task<string> Login(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwtToken(int userId)
        {
            AuthOptions authOptions = AppSettings.GetModel<AuthOptions>("AuthOptions");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authOptions.Security);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(authOptions.Expiration),
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
