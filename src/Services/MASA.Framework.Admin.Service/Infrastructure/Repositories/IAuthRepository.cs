using MASA.Framework.Admin.Contracts.Order.Model;

namespace MASA.Framework.Admin.Service.Order.Infrastructure.Repository
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginModel loginModel);

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        string GenerateJwtToken(int userId);
    }
}
