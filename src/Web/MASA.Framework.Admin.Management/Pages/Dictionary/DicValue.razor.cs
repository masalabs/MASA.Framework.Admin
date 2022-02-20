using MASA.Framework.Admin.Caller.Callers;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Model;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

namespace MASA.Framework.Admin.Management.Pages.Dictionary
{
    public partial class DicValue : ProCompontentBase
    {
        private DicValuePagingOptions _options = new();
        private int _totalCount = 0;
        private bool _loading = false;
        private List<DicValueViewModel> _tableData = new();

        [Parameter]
        public string Id { get; set; }

        private List<DataTableHeader<DicValueViewModel>> _headers
        {
            get
            {
                return new()
                {
                    new()
                    { Text = T("Label"), Value = nameof(DicValueViewModel.Lable), Sortable = false },
                    new()
                    { Text = T("Value"), Value = nameof(DicValueViewModel.Value), Sortable = false },
                    new()
                    { Text = T("Describe"), Value = nameof(DicValueViewModel.Description), Sortable = false },
                    new()
                    { Text = T("Switch"), Value = nameof(DicValueViewModel.Enable), Sortable = false },
                    new()
                    { Text = T("CreateTime"), Value = nameof(DicValueViewModel.CreateTime), Sortable = false },
                    new()
                    { Text = T("Sort"), Value = nameof(DicValueViewModel.Sort), Sortable = false },
                    new()
                    { Text = T("Action"), Value = "actions", Width = 300, Sortable = false }
                };
            }
        }

        private DataModal<DicValueViewModel> _dataModal = new();

        private string _dialogTitle = string.Empty;
        [Inject] protected ManagementCaller ManagementCaller { get; set; }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await FetchList();

                StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task FetchList(int pageIndex = 1, int pageSize = 10)
        {
            if (!Guid.TryParse(Id, out _))
            {
                return;
            }

            _options.PageIndex = pageIndex;
            _options.PageSize = pageSize;
            _options.DicId = Guid.Parse(Id);

            _loading = true;

            var result = await ManagementCaller.DicValueService.PagingAsync(_options);

            _tableData = result.Data;
            _totalCount = (int)result.TotalCount;

            _loading = false;
        }

        /// <summary>
        /// 新增
        /// </summary>
        private async Task AddAsync()
        {
            _dataModal.Visible = true;
            _dialogTitle = "新增";
        }

        private async Task Save()
        {
            if (!Guid.TryParse(Id, out _))
            {
                Message("跳转参数有误", AlertTypes.Warning);
                return;
            }

            if (!_dataModal.HasValue)
            {
                await ManagementCaller.DicValueService.CreateAsync(new AddDicValueModel
                {
                    Description = _dataModal.Data.Description,
                    Enable = _dataModal.Data.Enable,
                    Lable = _dataModal.Data.Lable,
                    Sort = _dataModal.Data.Sort,
                    Value = _dataModal.Data.Value,
                    DicId = Guid.Parse(Id)
                });

                Message("新增成功", AlertTypes.Success);
            }
            else
            {
                await ManagementCaller.DicValueService.UpdateAsync(new UpdateDicValueModel
                {
                    Id = _dataModal.Data.Id,
                    DicId = Guid.Parse(Id),
                    Description = _dataModal.Data.Description,
                    Enable = _dataModal.Data.Enable,
                    Lable = _dataModal.Data.Lable,
                    Sort = _dataModal.Data.Sort,
                    Value = _dataModal.Data.Value,
                });

                Message("更新成功", AlertTypes.Success);
            }

            _dataModal.Hide();

            await FetchList();
        }

        private async Task HandleOnOptionsUpdate(DataOptions options)
        {
            await FetchList(options.Page, options.ItemsPerPage);
        }

        public async Task UpdateAsync(DicValueViewModel model)
        {
            _dialogTitle = "更新";
            _dataModal.Visible = true;
            _dataModal.Show(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task DelAsync(DicValueViewModel model)
        {
            Confirm(
                   title: "删除字典",
                   content: $"您确认要删除吗？",
                   onOk: async () =>
                   {
                       await ManagementCaller.DicValueService.DeleteAsync(model.Id);

                       Message("删除成功", AlertTypes.Success);

                       await FetchList();

                       StateHasChanged();
                   }, AlertTypes.Warning);
        }
    }
}
