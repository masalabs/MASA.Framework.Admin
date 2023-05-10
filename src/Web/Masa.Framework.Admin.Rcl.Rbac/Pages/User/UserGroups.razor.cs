namespace Masa.Framework.Admin.Rcl.Rbac.Pages.User
{
    public partial class UserGroups
    {
        private bool _visible;
        private bool _valid = true;
        private MForm _form = new();
        private PaginationPage<UserGroupItemResponse> _pageData = new();
        private CreateGroupRequest _createUserGroup = new();
        private List<int> _pageSizes = new() { 10, 25, 50, 100 };
        private readonly List<DataTableHeader<UserGroupItemResponse>> _headers = new()
        {
            new() { Text = "名称", Value = nameof(UserGroupItemResponse.Name), CellClass = "" },
            new() { Text = "标识", Value = nameof(UserGroupItemResponse.Code) },
            new() { Text = "描述", Value = nameof(UserGroupItemResponse.Describtion) },
            new() { Text = "创建时间", Value = nameof(UserGroupItemResponse.CreationTime) },
            new() { Text = "最近修改时间", Value = nameof(UserGroupItemResponse.ModificationTime) },
            new() { Text = "操作", Value = "Action", Sortable = false }
        };

        [Inject]
        public UserGroupCaller UserGroupCaller { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private void NavToDetails(string id)
        {
            Nav.NavigateTo($"/usergroupDetail/{id}");
        }

        private async Task LoadData()
        {
            var dataRes = await UserGroupCaller.GetListAsync(_pageData.PageIndex, _pageData.PageSize, _pageData.Name ?? "");
            HandleCaller(dataRes, (data) =>
            {
                _pageData.PageData = data.Items.ToList();
                _pageData.CurrentCount = data.Count;
            });
        }

        private async Task CreateGroup(FormContext context)
        {
            var success = context.Validate();
            if (!success)
            {
                return;
            }

            await HandleCallerAsync(await UserGroupCaller.CreateAsync(_createUserGroup), async () =>
            {
                _visible = false;
                await LoadData();
            });
        }
    }
}
