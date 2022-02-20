using MASA.Framework.Admin.Contracts.Login.Model;

namespace MASA.Framework.Admin.Caller.UserCallers
{
    public class UserCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; } = "http://localhost:5041";

        public UserCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = "Login";
        }

        public async Task<LoginViewModel> Login(LoginModel loginModel)
        {
            return await CallerProvider.SendAsync<LoginModel, LoginViewModel>(HttpMethod.Post, $"api/User/Login", loginModel);
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            return await CallerProvider.SendAsync<int, UserModel>(HttpMethod.Get, $"api/User/GetUser", id);
        }
    }
}
