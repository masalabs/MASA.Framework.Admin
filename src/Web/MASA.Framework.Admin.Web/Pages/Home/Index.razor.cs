using Masa.Framework.Admin.Web.Shared;

namespace Masa.Framework.Admin.Web.Pages.Home
{
    public partial class Index
    {
        [CascadingParameter]
        public MainLayout MainLayout { get; set; } = default!;

        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                NavigationManager.NavigateTo(GlobalVariables.DefaultRoute);
            }
        }
    }
}
