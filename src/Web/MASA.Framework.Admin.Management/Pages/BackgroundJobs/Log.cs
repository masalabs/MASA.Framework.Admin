namespace MASA.Framework.Admin.Management.Pages.BackgroundJobs
{
    public partial class Log : ProCompontentBase
    {
        private JobLogPagingOptions _options = new();
        private int _totalCount = 0;
        private bool _loading = false;
        private List<JobLogViewModel> _tableData = new();

        [Parameter]
        public string Id { get; set; }

        private readonly List<DataTableHeader<JobLogViewModel>> _headers = new()
        {
            new()
            { Text = "任务名称", Value = nameof(JobLogViewModel.JobName), Sortable = false },
            new()
            { Text = "任务结果", Value = nameof(JobLogViewModel.JobResult), Sortable = false },
            new()
            { Text = "执行时间", Value = nameof(JobLogViewModel.CreateTime), Sortable = false }
        };

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
            _options.JobId = Guid.Parse(Id);

            _loading = true;

            var result = await ManagementCaller.JobService.LogPagingAsync(_options);

            _tableData = result.Data;
            _totalCount = (int)result.TotalCount;

            _loading = false;
        }
    }
}
