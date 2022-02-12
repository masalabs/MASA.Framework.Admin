using MASA.Framework.Admin.Caller.Callers;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Model;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;

namespace MASA.Framework.Admin.Management.Pages.Dictionary
{
    public partial class Dic : ProCompontentBase
    {
        private DicPagingOptions _options = new();
        private int _totalCount = 0;
        private bool _loading = false;
        private List<DicViewModel> _tableData = new();

        private readonly List<DataTableHeader<DicViewModel>> _headers = new()
        {
            new()
            { Text = "名称", Value = nameof(DicViewModel.Name), Sortable = false },
            new()
            { Text = "类型", Value = nameof(DicViewModel.Type), Sortable = false },
            new()
            { Text = "描述", Value = nameof(DicViewModel.Description), Sortable = false },
            new()
            { Text = "启用", Value = nameof(DicViewModel.Enable), Sortable = false },
            new()
            { Text = "创建时间", Value = nameof(DicViewModel.CreateTime), Sortable = false },
            new()
            { Text = "操作", Value = "actions", Width = 300, Sortable = false }
        };

        private DataModal<DicViewModel> _dataModal = new();

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
            _options.PageIndex = pageIndex;
            _options.PageSize = pageSize;

            _loading = true;



            var result = await ManagementCaller.DicService.PagingAsync(_options);

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
            if (!_dataModal.HasValue)
            {
                await ManagementCaller.DicService.CreateAsync(new AddDicModel
                {
                    Description = _dataModal.Data.Description,
                    Enable = _dataModal.Data.Enable,
                    ModuleId = _dataModal.Data.ModuleId,
                    Name = _dataModal.Data.Name,
                    Type = _dataModal.Data.Type,
                });

                Message("新增成功", AlertTypes.Success);
            }
            else
            {
                await ManagementCaller.DicService.UpdateAsync(new UpdateDicModel
                {
                    Id = _dataModal.Data.Id,
                    Description = _dataModal.Data.Description,
                    Enable = _dataModal.Data.Enable,
                    ModuleId = _dataModal.Data.ModuleId,
                    Name = _dataModal.Data.Name,
                    Type = _dataModal.Data.Type,
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

        public async Task UpdateAsync(DicViewModel model)
        {
            _dialogTitle = "更新";
            _dataModal.Visible = true;
            _dataModal.Show(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task DelAsync(DicViewModel model)
        {
            Confirm(
                   title: "删除字典",
                   content: $"您确认要删除吗？",
                   onOk: async () =>
                   {
                       await ManagementCaller.DicService.DeleteAsync(model.Id);

                       Message("删除成功", AlertTypes.Success);

                       await FetchList();

                       StateHasChanged();
                   }, AlertTypes.Warning);
        }

        public void DetailList(DicViewModel model) => NavigationManager.NavigateTo($"/management-admin/dicValue-list/{model.Id}");
    }
}
