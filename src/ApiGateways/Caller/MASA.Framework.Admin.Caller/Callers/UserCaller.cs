using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Utils.Caller.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Callers
{
    public class UserCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; } = "http://localhost:6383";

        public UserCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(UserCaller);
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            return await CallerProvider.SendAsync<LoginModel, string>(HttpMethod.Post, $"api/User/Login", loginModel);
        }

        public async Task<UserModel> GetUserAsync(int id)
        {
            return await CallerProvider.SendAsync<int, UserModel>(HttpMethod.Get, $"api/User/GetUser", id);
        }
    }
}
