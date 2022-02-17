using MASA.Framework.Admin.Web.Shared;

namespace MASA.Framework.Admin.Web.Pages.Home
{
    public partial class Index
    {
        [CascadingParameter]
        public MainLayout MainLayout { get; set; } = default!;

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var token = HttpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Token");
                if (token == null)
                {
                    NavigationManager.NavigateTo("/pages/authentication/Login-v2", true);
                }
                else
                {
                    await MainLayout.StartSignalR(token.Value);

                    NavigationManager.NavigateTo(GlobalVariables.DefaultRoute);
                }
            }
        }
    }
}
