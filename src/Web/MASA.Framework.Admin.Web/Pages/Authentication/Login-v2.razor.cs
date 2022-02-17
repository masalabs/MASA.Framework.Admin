using MASA.Framework.Admin.Caller.Callers;
using MASA.Framework.Admin.Contracts.Order.Model;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR;
using MASA.Framework.Admin.Caller.UserCallers;
using MASA.Framework.Admin.Contracts.Login.Model;
using MASA.Framework.Admin.Web.Shared;

namespace MASA.Framework.Admin.Web.Pages.Authentication
{
    public partial class Login_v2
    {
        private HubConnection _hubConnection = default!;
        private readonly string _hubName = "login";
        private bool _show;
        private string _account = "";
        private string _password = "";
        private string _errorMessage = "";
        private bool _showErrorMessage;
        private bool _loading;
        private Func<string, StringBoolean> _requiredRule = value => !string.IsNullOrEmpty(value) ? true : "Required.";
        private IEnumerable<Func<string, StringBoolean>> _requiredRules => new List<Func<string, StringBoolean>>
        {
            _requiredRule,
        };

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public UserCaller UserCaller { get; set; } = default!;

        [CascadingParameter]
        public MainLayout App { get; set; } = default!;

        private async Task LoginAsync()
        {
            _loading = true;
            var tokenInfo = await GetToken();

            if (tokenInfo == null || tokenInfo.Code == 1)
            {
                _loading = false;
            }
            else if (tokenInfo.Code == 0)
            {
                try
                {
                    //string huburl = NavigationManager.BaseUri.TrimEnd('/') + $"/{_hubName}";
                    //_hubConnection = new HubConnectionBuilder()
                    //    .WithUrl("http://localhost:5041/login", options =>
                    //    {
                    //        options.AccessTokenProvider = () => Task.FromResult<string?>(tokenInfo.Result);
                    //    })
                    //    .WithAutomaticReconnect()
                    //    .Build();

                    //_hubConnection.On<string, string>("Logout", Logout);

                    //await _hubConnection.StartAsync();

                    //await App.StartSignalR(tokenInfo.Result);

                    _loading = false;

                    NavigationManager.NavigateTo($"/Account/Login?token={tokenInfo.Result}", true);
                }
                catch (Exception)
                {
                    _loading = false;
                    throw;
                }
                finally
                {
                    _loading = false;
                }
            }
        }

        private void Logout(string name, string msg)
        {

        }

        private async Task<LoginViewModel?> GetToken()
        {
            if (!string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_password))
            {
                LoginViewModel loginViewModel = await UserCaller.Login(new LoginModel
                {
                    Account = _account,
                    Password = _password
                });

                if (loginViewModel.Code == 1)
                {
                    _showErrorMessage = true;
                    _errorMessage = loginViewModel.Result;
                }
                else
                {
                    _showErrorMessage = false;
                }

                return loginViewModel;
            }
            else
            {
                _showErrorMessage = true;
                _errorMessage = "请输入账号或密码";
                return null;
            }
        }
    }
}
