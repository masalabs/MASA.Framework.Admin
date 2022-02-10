using MASA.Framework.Admin.Contracts.Order.Model;

namespace MASA.Framework.Admin.Service.Login.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<string> LoginAsync(LoginModel loginModel);

        Task<UserModel> GetUserAsync(int id);
    }
}
