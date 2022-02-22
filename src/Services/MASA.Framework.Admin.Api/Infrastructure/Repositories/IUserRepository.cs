using MASA.Framework.Admin.Contracts.Login.Model;

namespace MASA.Framework.Admin.Service.Api.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<LoginViewModel> LoginAsync(LoginModel loginModel);

        Task<UserModel> GetUserAsync(int id);

        Task<int> GetUserCount();
    }
}
