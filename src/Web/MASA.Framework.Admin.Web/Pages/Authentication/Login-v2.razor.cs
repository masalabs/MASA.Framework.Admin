using MASA.Framework.Admin.Caller.UserCallers;
using MASA.Framework.Admin.Contracts.Login.Model;
using Microsoft.AspNetCore.SignalR.Client;

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
        public MASA.Framework.Admin.Caller.UserCallers.UserCaller UserCaller { get; set; } = default!;


        private async Task LoginAsync()
        {
            _loading = true;
            var token = await GetToken();

            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    string huburl = NavigationManager.BaseUri.TrimEnd('/') + $"/{_hubName}";
                    _hubConnection = new HubConnectionBuilder()
                        .WithUrl("http://localhost:5041/login", options =>
                        {
                            options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                        })
                        .WithAutomaticReconnect()
                        .Build();

                    _hubConnection.On<string, string>("Logout", Logout);

                    await _hubConnection.StartAsync();

                    _loading = false;

                    NavigationManager.NavigateTo($"/Account/Login?token={token}", true);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void Logout(string name, string msg)
        {

        }

        private async Task<string> GetToken()
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
                    return "";
                }
                else
                {
                    _showErrorMessage = false;
                    return loginViewModel.Result;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
