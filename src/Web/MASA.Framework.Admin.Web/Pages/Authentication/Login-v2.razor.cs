using MASA.Framework.Admin.Web.Shared;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MASA.Framework.Admin.Web.Pages.Authentication
{
    public partial class Login_v2
    {
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
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; } = default!;

        [Inject]
        public UserCaller UserCaller { get; set; } = default!;

        [CascadingParameter]
        public MainLayout App { get; set; } = default!;

        private async Task LoginAsync()
        {
            _loading = true;
            var token = await GetToken();
            _loading = false;
            if (!string.IsNullOrEmpty(token))
            {
                await ProtectedLocalStorage.SetAsync("IsLogined", true);
                NavigationManager.NavigateTo($"/Account/Login?token={token}", true);
            }
        }

        private async Task<string> GetToken()
        {
            if (!string.IsNullOrWhiteSpace(_account) && !string.IsNullOrWhiteSpace(_password))
            {
                var loginRes = await UserCaller.LoginAsync(_account, _password);

                if (!loginRes.Success)
                {
                    _showErrorMessage = true;
                    _errorMessage = loginRes.Message;
                }
                else
                {
                    _showErrorMessage = false;
                }

                return loginRes.Data;
            }
            else
            {
                _showErrorMessage = true;
                _errorMessage = "请输入账号或密码";
                return string.Empty;
            }
        }
    }
}
