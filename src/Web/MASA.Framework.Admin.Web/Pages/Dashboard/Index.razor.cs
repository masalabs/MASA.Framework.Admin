using MASA.Framework.Admin.Models;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Web.Pages.Dashboard
{
    public partial class Index
    {
        private HttpClient _httpClient;
        private IEnumerable<OperationLogDto> _operationLogs;
        private int _count;
        private IEnumerable<VisitPageDayStatistics> _visitPageDayStatistics;
        private IEnumerable<VisitPageHourStatistics> _visitPageHourStatistics;
        private dynamic _option = new
        {
            Title = new
            {
                Text = "浏览量(PV)"
            },
            Tooltip = new
            {
                Trigger = "axis"
            },
            Legend = new
            {
                Data = new[] { "昨日", "今日" }
            },
            Grid = new
            {
                Left = "3%",
                Right = "4%",
                Bottom = "3%",
                ContainLabel = true
            },
            Toolbox = new
            {
                Feature = new
                {
                    SaveAsImage = new
                    {
                    }
                }
            },
            XAxis = new
            {
                Type = "category",
                BoundaryGap = false,
                Data = new[] { "0", "", "", "", "4", "", "", "", "8", "", "", "", "12", "", "", "", "16", "", "", "", "20", "", "", "" }
            },
            YAxis = new
            {
                Type = "value"
            },
            Series = new[]
            {
                new
                {
                    Name= "昨日",
                    Type= "line",
                    Stack="Total",
                    Data= new int[24]
                },
                new
                {
                    Name= "今日",
                    Type= "line",
                    Stack="Total",
                    Data= new int[24]
                }
            }
        };

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        protected VisitPageDayStatistics TodayStatistics => _visitPageDayStatistics.FirstOrDefault(statistic => statistic.Date == DateTime.Today);

        protected VisitPageDayStatistics YesterdayStatistics => _visitPageDayStatistics.FirstOrDefault(statistic => statistic.Date == DateTime.Today.AddDays(-1));

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("MASA.Framework.Admin.Api");

            await UpdateOperationLogsAsync();
            await UpdateVisitPageDayStatisticsAsync();
            await UpdateVisitPageHourStatisticsAsync();
        }

        private async Task UpdateOperationLogsAsync(int offset = 1, int limit = 10)
        {
            var query = $"?offset={offset}&limit={limit}";
            var pageResult = await _httpClient.GetFromJsonAsync<PageResult<OperationLogDto>>("/operationLog" + query);

            _operationLogs = pageResult.Data;
            _count = pageResult.Count;
        }

        private async Task UpdateVisitPageDayStatisticsAsync()
        {
            var startDate = DateTime.Today.AddDays(-1);
            var endDate = DateTime.Today;
            var query = $"?startDate={startDate}&endDate={endDate}";

            _visitPageDayStatistics = await _httpClient.GetFromJsonAsync<IEnumerable<VisitPageDayStatistics>>("/api/visitPageDayStatistics" + query);
        }

        private async Task UpdateVisitPageHourStatisticsAsync()
        {
            var startTime = DateTime.Today;
            var endTime = DateTime.Today.AddDays(1);
            var query = $"?startTime={startTime}&endTime={endTime}";

            _visitPageHourStatistics = await _httpClient.GetFromJsonAsync<IEnumerable<VisitPageHourStatistics>>("/api/visitPageHourStatistics" + query);

            UpdateData(DateTime.Today.AddDays(-1), 0);//Yesterday
            UpdateData(DateTime.Today, 1);//Today

            void UpdateData(DateTime date, int day)
            {
                var statistics = _visitPageHourStatistics
                                .Where(statistic => statistic.Time.Date == date);

                for (int i = 0; i < 24; i++)
                {
                    _option.Series[day].Data[i] = statistics.FirstOrDefault(statistic => statistic.Time.Hour == i)?.PV ?? 0;
                }
            }
        }

        private async ValueTask<ItemsProviderResult<OperationLogDto>> LoadOperationLogs(ItemsProviderRequest request)
        {
            var offset = request.StartIndex;
            var limit = Math.Min(request.Count, _count - request.StartIndex);

            await UpdateOperationLogsAsync(offset, limit);
            return new ItemsProviderResult<OperationLogDto>(_operationLogs, _count);
        }
    }
}
