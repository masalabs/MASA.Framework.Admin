namespace Masa.Framework.Admin.RCL.RBAC.Pages.User
{
    public partial class UserGroup
    {
        private bool _visible;
        private bool _valid = true, _snackbar = false;
        private MForm _form = new();
        private PaginationPage<UserGroupItemResponse> _pageData = new();
        private UserGroupItemResponse _userItem = new();
        private CreateUserModel _createUserModel = new();
        private List<int> _pageSizes = new() { 10, 25, 50, 100 };
        private readonly List<DataTableHeader<UserGroupItemResponse>> _headers = new()
        {
            new() { Text = "名称", Value = nameof(UserGroupItemResponse.Name), CellClass = "" },
            new() { Text = "Code", Value = nameof(UserGroupItemResponse.Code) },
            new() { Text = "描述", Value = nameof(UserGroupItemResponse.Describtion) },
            new() { Text = "创建时间", Value = nameof(UserGroupItemResponse.CreationTime) },
            new() { Text = "最近修改时间", Value = nameof(UserGroupItemResponse.ModificationTime) },
            new() { Text = "Action", Value = "Action", Sortable = false }
        };

        [Inject]
        public UserCaller UserCaller { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            var dataRes = await UserCaller.GetListAsync(_pageData.PageIndex, _pageData.PageSize, _pageData.Name ?? "", _pageData.State);

            if (dataRes.Success && dataRes.Data != null)
            {
                //_pageData.PageData = dataRes.Data.Items.ToList();
                //_pageData.CurrentCount = dataRes.Data.Count;
            }
        }


        private async Task DeleteUser(string id)
        {
            var res = await UserCaller.DeleteAsync(id);
            if (!res.Success)
            {
                //tip msg
            }
            else
            {
                //reload items
            }
        }

        private async Task CreateUser(EditContext context)
        {
            var success = context.Validate();
            if (!success)
            {
                return;
            }
            if (!_createUserModel.Pwd.Trim().Equals(_createUserModel.ConfirmPwd.Trim()))
            {
                //密码确认失败 提示
                return;
            }

            var res = await UserCaller.CreateAsync(_createUserModel);
            if (!res.Success)
            {
                _snackbar = true;
            }

            _visible = false;
            await LoadData();
        }
    }
}
