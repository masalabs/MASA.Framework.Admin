using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Framework.Admin.Service.Login.Infrastructure.Utils;
using MASA.Framework.Admin.Service.Order.Infrastructure.Repository;

namespace MASA.Framework.Admin.Service.Login.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IAuthRepository _authRepository;

        public UserRepository(MyDbContext dbContext, IAuthRepository authRepository)
        {
            _dbContext = dbContext;
            _authRepository = authRepository;
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            var result = await _dbContext.Users.FindAsync(id);

            if (result == null)
            {
                throw new Exception("用户不存在!");
            }

            return new UserModel
            {
                Id = result.Id,
                Account = result.Account,
                NickName = result.NickName,
                State = result.State
            };
        }

        public async Task<LoginViewModel> LoginAsync(LoginModel loginModel)
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Account == loginModel.Account);
            if (user == null)
            {
                loginViewModel.Code = 1;
                loginViewModel.Result = "该账号不存在！";
            }
            else if (user.State == 0)
            {
                loginViewModel.Code = 1;
                loginViewModel.Result = "该账号已禁用！";
            }
            else
            {
                string token = "";
                var pwd = SHA1Utils.Encrypt($"{loginModel.Password}{user.Salt}");
                if (pwd == user.Password)
                {
                    token = _authRepository.GenerateJwtToken(user.Id);
                    loginViewModel.Code = 0;
                    loginViewModel.Result = token;
                }
                else
                {
                    loginViewModel.Code = 1;
                    loginViewModel.Result = "密码错误！";
                }
            }

            return loginViewModel;
        }
    }
}
