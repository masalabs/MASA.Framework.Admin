using MASA.Framework.Admin.Caller.Callers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace MASA.Framework.Admin.Web.Pages
{
    public partial class Login1
    {
        [Inject]
        public UserCaller UserCaller { get; set; } = default!;

        private HubConnection _hubConnection = default!;
        private string _hubName = "login";

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private async Task LoginAsync()
        {
            var token = await GetToken();

            
            try
            {
                string huburl = NavigationManager.BaseUri + $"/{_hubName}";
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl("http://localhost:6383/login", options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult<string?>(token);
                    })
                    .WithAutomaticReconnect()
                    .Build();

                //_hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
                //{
                //    Message(user, message);
                //});

                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<string> GetToken()
        {
            var result= await UserCaller.Login(new Contracts.Order.Model.LoginModel
            {
                Account = "admin",
                Password = "admin"
            });

            return result;
        }

        private void Message(string user, string message)
        {
            Console.WriteLine();
        }
    }
}
