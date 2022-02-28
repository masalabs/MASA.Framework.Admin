namespace Masa.Framework.Admin.RCL.RBAC.Pages.Organization
{
    public partial class Org
    {
        bool _addOrgDialog, _addDepartmentUserDialog;
        List<Guid> _active = new List<Guid>();
        PaginationPage<UserItemResponse> _pageData = new();
        List<UserItemResponse> _departmentUsers = new();
        List<DepartmentItemResponse> _departments = new();
        CreateDepartmentRequest _createDepartment = new();
        DepartmentItemResponse _currentDepartment = new();
        readonly List<DataTableHeader<UserItemResponse>> _headers = new()
        {
            new() { Text = "Name", Value = nameof(UserItemResponse.Name) },
            new() { Text = "Email", Value = nameof(UserItemResponse.Email) },
            new() { Text = "State", Value = nameof(UserItemResponse.State) },
            new() { Text = "Gender", Value = nameof(UserItemResponse.Gender) },
            new() { Text = "Action", Value = "Action", Sortable = false }
        };

        [Parameter]
        public Guid OrgId { get; set; } = Guid.Empty;

        [Inject]
        public OrganizationCaller OrganizationCaller { get; set; } = null!;

        [Inject]
        public UserCaller UserCaller { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDataAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }


        private async Task LoadDataAsync()
        {
            var res = await OrganizationCaller.GetListAsync(OrgId);
            if (res.Success)
            {
                _departments = res.Data;
                StateHasChanged();
            }
        }

        private async Task OpenAddDialog(Guid parentId, string parentName)
        {
            _createDepartment = new()
            {
                ParentId = parentId,
                ParentName = parentName
            };
            _addOrgDialog = true;
        }

        private async Task AddDepartment()
        {
            var res = await OrganizationCaller.CreateAsync(_createDepartment);

            if (res.Success)
            {
                _addOrgDialog = false;
                await LoadDataAsync();
            }
        }

        private async Task LoadDepartUser(DepartmentItemResponse departmentItem)
        {
            _currentDepartment = departmentItem;
            var res = await UserCaller.GetUsersWithDepartmentAsync(_currentDepartment.Id, true);
            if (res.Success && res.Data != null)
            {
                _departmentUsers = res.Data;
            }
        }

        private async Task OpenDepartmentUserDialog()
        {
            //var res = await UserCaller.GetUsersWithDepartmentAsync(_currentDepartment.Id, true);
            //if (res.Success && res.Data != null)
            //{
            //    _departmentUsers = res.Data;
            //}
        }
    }
}
