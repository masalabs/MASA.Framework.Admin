namespace Masa.Framework.Admin.RCL.RBAC.Pages.Organization
{
    public partial class Org
    {
        bool _addOrgDialog;
        List<Guid> _active = new List<Guid>();
        List<DepartmentItemResponse> _dapartments = new();
        CreateDepartmentRequest _createDepartment = new();
        readonly List<DataTableHeader<UserItemResponse>> _headers = new()
        {
            new() { Text = "Name", Value = nameof(UserItemResponse.Name) },
            new() { Text = "Email", Value = nameof(UserItemResponse.Email) },
            new() { Text = "State", Value = nameof(UserItemResponse.State) },
            new() { Text = "Gender", Value = nameof(UserItemResponse.Gender) },
            new() { Text = "Action", Value = "Action", Sortable = false }
        };

        [Parameter]
        public Guid? OrgId { get; set; }

        [Inject]
        public OrganizationCaller OrganizationCaller { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task AddDepartment()
        {

        }

        private async Task LoadDepartUser(DepartmentItemResponse departmentItem)
        {

        }
    }
}
