namespace Masa.Framework.Admin.Rcl.Rbac.Pages.Organization
{
    public partial class Org
    {
        bool _addOrgDialog, _addDepartmentUserDialog, _disableDepartmentMemberBtn = true;
        List<Guid> _active = new List<Guid>();
        List<UserItemResponse> _departmentUsers = new();
        List<DepartmentItemResponse> _departments = new();
        CreateDepartmentRequest _createDepartment = new();
        string _currentDepartmentName = "";
        Guid _currentDepartmentId = Guid.Empty;
        readonly List<DataTableHeader<UserItemResponse>> _headers = new()
        {
            new() { Text = "姓名", Value = nameof(UserItemResponse.Name) },
            new() { Text = "邮箱", Value = nameof(UserItemResponse.Email) },
            new() { Text = "状态", Value = nameof(UserItemResponse.State) },
            new() { Text = "性别", Value = nameof(UserItemResponse.Gender) },
            new() { Text = "操作", Value = "Action", Sortable = false }
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
            HandleCaller(res, (data) =>
            {
                _departments = data;
                StateHasChanged();
            });
        }

        private void OpenAddDialog(Guid parentId, string parentName)
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
            await HandleCallerAsync(res, async () =>
            {
                _addOrgDialog = false;
                await LoadDataAsync();
            });
        }

        private async Task ActiveUpdated(List<DepartmentItemResponse> activedItems)
        {
            _disableDepartmentMemberBtn = false;
            _currentDepartmentId = activedItems[0].Id;
            _currentDepartmentName = activedItems[0].Name;
            var res = await UserCaller.GetUsersWithDepartmentAsync(_currentDepartmentId, true);
            HandleCaller(res, (data) =>
            {
                _departmentUsers = data;
            });
        }

        private async Task UpdateDepartmentUser()
        {
            var res = await OrganizationCaller.UpdateDepartmentUsers(new UpdateDepartmentUserRequest
            {
                DepartmentId = _currentDepartmentId,
                UserIds = _departmentUsers.Where(u => u.Select).Select(u => u.Id).ToList()
            });
            HandleCaller(res, () =>
            {
                _addDepartmentUserDialog = false;
            });
        }
    }
}
