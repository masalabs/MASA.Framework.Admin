using MASA.Framework.Admin.Contracts.Order.Model;

namespace MASA.Framework.Admin.Service.Login.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<LoginViewModel> LoginAsync(LoginModel loginModel);

        Task<UserModel> GetUserAsync(int id);
    }
}
