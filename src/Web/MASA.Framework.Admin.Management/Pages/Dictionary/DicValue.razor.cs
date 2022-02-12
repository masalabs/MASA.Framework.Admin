﻿using MASA.Framework.Admin.Caller.Callers;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Model;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

namespace MASA.Framework.Admin.Management.Pages.Dictionary
{
    public partial class DicValue:ProCompontentBase
    {
        private DicValuePagingOptions _options = new();
        private int _totalCount = 0;
        private bool _loading = false;
        private List<DicValueViewModel> _tableData = new();

        private readonly List<DataTableHeader<DicValueViewModel>> _headers = new()
        {
            new()
            { Text = "标签", Value = nameof(DicValueViewModel.Lable), Sortable = false },
            new()
            { Text = "数据值", Value = nameof(DicValueViewModel.Value), Sortable = false },
            new()
            { Text = "描述", Value = nameof(DicValueViewModel.Description), Sortable = false },
            new()
            { Text = "启用", Value = nameof(DicValueViewModel.Enable), Sortable = false },
            new()
            { Text = "创建时间", Value = nameof(DicValueViewModel.CreateTime), Sortable = false },
            new()
            { Text = "排序", Value = nameof(DicValueViewModel.Sort), Sortable = false },
            new()
            { Text = "操作", Value = "actions", Width = 300, Sortable = false }
        };

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
            _options.PageIndex = pageIndex;
            _options.PageSize = pageSize;

            _loading = true;

            _tableData = new List<DicValueViewModel>
            {
                new DicValueViewModel
                {
                     Lable = "测试1",
                    Value = "测试1",
                    Description = "测试1",
                    Enable = true,
                    CreateTime = DateTime.Now,
                },
                new DicValueViewModel
                {
                    Lable = "测试2",
                    Value = "测试2",
                    Description = "测试2",
                    Enable = false,
                    CreateTime = DateTime.Now,
                },
                new DicValueViewModel
                {
                    Lable = "测试3",
                    Value = "测试3",
                    Description = "测试3",
                    Enable = true,
                    CreateTime = DateTime.Now,
                },
            };
            _totalCount = 3;

            //var result = await ManagementCaller.DictionaryService.PagingAsync(_options);

            //_tableData = result.Data;
            //_totalCount = (int)result.TotalCount;

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
                await ManagementCaller.DicValueService.CreateAsync(new AddDicValueModel
                {
                    Description = _dataModal.Data.Description,
                    Enable = _dataModal.Data.Enable,
                    Lable = _dataModal.Data.Lable,
                    Sort = _dataModal.Data.Sort,
                    Value = _dataModal.Data.Value,
                });

                Message("新增成功", AlertTypes.Success);
            }
            else
            {
                await ManagementCaller.DicValueService.UpdateAsync(new UpdateDicValueModel
                {
                    Id = _dataModal.Data.Id,
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

                       StateHasChanged();
                   }, AlertTypes.Warning);
        }
    }
}
