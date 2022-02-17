using Microsoft.AspNetCore.SignalR.Client;
using static MASA.Blazor.Presets.Message;

namespace MASA.Framework.Admin.Web.Shared
{
    public partial class MainLayout
    {
        private HubConnection _hubConnection = default!;
        private bool _show;
        private string _msg;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

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
        }

       
        public async Task Logout(string msg)
        {
            await _hubConnection.StopAsync();
            await _hubConnection.DisposeAsync();

            _show = true;
            _msg = msg;

            StateHasChanged();
        }

        public void OK()
        {
            NavigationManager.NavigateTo($"/Account/Logout", true);
        }
    }
}
