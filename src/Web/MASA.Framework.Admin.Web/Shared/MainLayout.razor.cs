using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

namespace MASA.Framework.Admin.Web.Shared
{
    public partial class MainLayout
    {
        private HubConnection _hubConnection = default!;
        private bool _show;
        private string _msg = "";

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        public ProtectedLocalStorage ProtectedLocalStorage { get; set; } = default!;

        [Inject]
        public IConfiguration Configuration { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var token = HttpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Token");
                bool isLogined = false;
                try
                {
                    isLogined = (await ProtectedLocalStorage.GetAsync<bool>("IsLogined")).Value;

                }catch(Exception e)
                {

                }
                
                if (token == null || !isLogined)
                {
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
                .WithUrl($"{Configuration["ApiGateways:UserCaller"]}/hub/login", HttpTransportType.WebSockets | HttpTransportType.LongPolling
                    , options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                    }
                )
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Information);
                    logging.AddConsole();
                })
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string>("Logout", Logout);

            //await _hubConnection.StartAsync();
        }

        public async Task Logout(string msg)
        {
            await ProtectedLocalStorage.SetAsync("IsLogined", false);

            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();

            _show = true;
            _msg = msg;

            StateHasChanged();
            await Task.Delay(5000);

            GoToLogout();
        }

        public void GoToLogout()
        {
            NavigationManager.NavigateTo($"/Account/Logout", true);
        }
    }
}
