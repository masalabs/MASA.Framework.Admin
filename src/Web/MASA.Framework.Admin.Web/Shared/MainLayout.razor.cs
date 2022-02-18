using Microsoft.AspNetCore.SignalR.Client;
using static MASA.Blazor.Presets.Message;

namespace MASA.Framework.Admin.Web.Shared
{
    public partial class MainLayout
    {
        private HubConnection _hubConnection = default!;
        private bool _show;
        private string _msg = "";
        private static bool _isLogouted;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var token = HttpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Token");
                if (token == null || _isLogouted)
                {
                    _isLogouted = false;
                    NavigationManager.NavigateTo("/pages/authentication/Login-v2", true);
                }
                else
                {
                    await StartSignalR(token.Value);
                }
            }
        }

        public async Task StartSignalR(string token)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5041/login", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("Logout", Logout);

            await _hubConnection.StartAsync();

            _isLogouted = false;
        }


        public async Task Logout(string msg)
        {
            _isLogouted = true;

            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();

            _show = true;
            _msg = msg;

            StateHasChanged();
            await Task.Delay(3000);

            GoToLogout();
        }

        public void GoToLogout()
        {
            NavigationManager.NavigateTo($"/Account/Logout", true);
        }
    }
}
