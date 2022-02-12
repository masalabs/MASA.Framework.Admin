namespace MASA.Framework.Admin.Management.Pages.BackgroundJobs
{
    public partial class Index: ProCompontentBase
    {
        private JobPagingOptions _options = new();
        private int _totalCount = 0;
        private bool _loading = false;
        private List<JobViewModel> _tableData = new();

        private readonly List<DataTableHeader<JobViewModel>> _headers = new()
        {
            new()
            { Text = "名称", Value = nameof(JobViewModel.Name), Sortable = false },
            new()
            { Text = "方法", Value = nameof(JobViewModel.Method), Sortable = false },
            new()
            { Text = "参数", Value = nameof(JobViewModel.Args), Sortable = false },
            new()
            { Text = "执行次数", Value = nameof(JobViewModel.TryCount), Sortable = false },
            new()
            { Text = "执行周期(秒)", Value = nameof(JobViewModel.PeriodSeconds), Sortable = false },
            new()
            { Text = "启用", Value = nameof(JobViewModel.Enable), Sortable = false },
            new()
            { Text = "创建时间", Value = nameof(JobViewModel.CreateTime), Sortable = false },
            new()
            { Text = "操作", Value = "actions", Width = 300, Sortable = false }
        };

        private DataModal<JobViewModel> _dataModal = new();

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



            var result = await ManagementCaller.JobService.PagingAsync(_options);

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
                await ManagementCaller.JobService.CreateAsync(new AddJobRequest
                {
                    Name = _dataModal.Data.Name,
                    Method = _dataModal.Data.Method,
                    Args = _dataModal.Data.Args,
                    PeriodSeconds = _dataModal.Data.PeriodSeconds,
                    Enable = _dataModal.Data.Enable,
                });

                Message("新增成功", AlertTypes.Success);
            }
            else
            {
                await ManagementCaller.JobService.UpdateAsync(new UpdateJobRequest
                {
                    Id = _dataModal.Data.Id,
                    Name = _dataModal.Data.Name,
                    Method = _dataModal.Data.Method,
                    Args = _dataModal.Data.Args,
                    PeriodSeconds = _dataModal.Data.PeriodSeconds,
                    Enable = _dataModal.Data.Enable,
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

        public async Task UpdateAsync(JobViewModel model)
        {
            _dialogTitle = "更新";
            _dataModal.Visible = true;
            _dataModal.Show(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task DelAsync(JobViewModel model)
        {
            Confirm(
                   title: "删除定时任务",
                   content: $"您确认要删除吗？",
                   onOk: async () =>
                   {
                       await ManagementCaller.DicService.DeleteAsync(model.Id);

                       Message("删除成功", AlertTypes.Success);

                       await FetchList();

                       StateHasChanged();
                   }, AlertTypes.Warning);
        }

        public void LogList(JobViewModel model) => NavigationManager.NavigateTo($"/management-admin/job-log-list/{model.Id}");
    }
}
