using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Contracts.PageviewStatistics;
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
        private HttpClient _loggingClient;
        private HttpClient _pageviewStatisticsClient;
        private HttpClient _userClient;
        private IEnumerable<OperationLogDto> _operationLogs;
        private int _onlineUserCount;
        private IEnumerable<PageviewDayStatistics> _pageviewDayStatistics;
        private IEnumerable<PageviewHourStatistics> _pageviewHourStatistics;
        private StringNumber _current = "PV";
        private List<DataTableHeader<OperationLogDto>> _headers = new List<DataTableHeader<OperationLogDto>>
        {
           new ()
           {
                Text= "用户名",
                Align= "start",
                Sortable= false,
                Value= nameof(OperationLogDto.Username),
                CellClass="text-subtitle"
          },
          new ()
          {
                Text= "描述",
                Value= nameof(OperationLogDto.Description),
                CellClass="text-subtitle"
          },
          new ()
          {
                Text= "时间",
                Value= nameof(OperationLogDto.CreateTime),
                CellClass="text-subtitle"}
        };
        private dynamic _onlineChart = new
        {
            Tooltip = new
            {
                Trigger = "item",
            },
            Series = new[]
                {
                new
                {
                    Type = "pie",
                    Radius = "90%",
                    Label = new
                    {
                        Show = false
                    },
                    Data = new[]
                       {
                        new
                        {
                            value = 20,
                            Name = "Online",
                            ItemStyle = new
                            {
                                Color = "rgb(67, 24, 255)"
                            }
                        },
                        new
                        {
                            value = 75,
                            Name = "Offline",
                            ItemStyle = new
                            {
                                Color = "rgb(161, 139, 255)"
                            }
                        }
                    }
                }
            }
        };
        private dynamic _option = new
        {
            Title = new
            {
                Text = ""
            },
            Tooltip = new
            {
                Trigger = "axis"
            },
            Legend = new
            {
                Data = new[] { "今日", "昨日" },
                Right = "5px",
                TextStyle = new
                {
                    Color = "#485585",
                }
            },
            Grid = new
            {
                Left = "3%",
                Right = "4%",
                Bottom = "3%",
                ContainLabel = true
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
                    Data= new int[24],
                    ItemStyle=new
                    {
                        Normal=new
                        {
                            Color="rgb(161, 139, 255)"
                        }
                    },
                    LineStyle=new
                    {
                        Normal=new
                        {
                            Color="rgb(161, 139, 255)"
                        }
                    }
                },
                new
                {
                    Name= "今日",
                    Type= "line",
                    Stack="Total",
                    Data= new int[24],
                    ItemStyle=new
                    {
                        Normal=new
                        {
                            Color="rgb(67, 24, 255)"
                        }
                    },
                    LineStyle=new
                    {
                        Normal=new
                        {
                            Color="rgb(67, 24, 255)"
                        }
                    }
                }
            }
        };

        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }

        protected PageviewDayStatistics TodayStatistics => _pageviewDayStatistics.FirstOrDefault(statistic => statistic.Date == DateTime.Today);

        protected PageviewDayStatistics YesterdayStatistics => _pageviewDayStatistics.FirstOrDefault(statistic => statistic.Date == DateTime.Today.AddDays(-1));

        protected override async Task OnInitializedAsync()
        {
            _loggingClient = HttpClientFactory.CreateClient("Logging");
            _pageviewStatisticsClient = HttpClientFactory.CreateClient("PageviewStatistics");
            _userClient = HttpClientFactory.CreateClient("User");

            await UpdateOperationLogsAsync(0, int.MaxValue);
            await UpdateVisitPageDayStatisticsAsync();
            await UpdateVisitPageHourStatisticsAsync();
            await UpdateOnlineUserCountAsync();
        }

        private async Task UpdateOperationLogsAsync(int offset = 0, int limit = 10)
        {
            var query = $"?offset={offset}&limit={limit}";
            var pageResult = await _loggingClient.GetFromJsonAsync<PageResult<OperationLogDto>>("/api/operationLog" + query);

            _operationLogs = pageResult.Data;
        }

        private async Task UpdateVisitPageDayStatisticsAsync()
        {
            var startDate = DateTime.Today.AddDays(-1);
            var endDate = DateTime.Today;
            var query = $"?startDate={startDate}&endDate={endDate}";

            _pageviewDayStatistics = await _pageviewStatisticsClient.GetFromJsonAsync<IEnumerable<PageviewDayStatistics>>("/api/pageviewDayStatistics" + query);
        }

        private async Task UpdateVisitPageHourStatisticsAsync()
        {
            var startTime = DateTime.Today.AddDays(-1);
            var endTime = DateTime.Today.AddDays(1);
            var query = $"?startTime={startTime}&endTime={endTime}";

            _pageviewHourStatistics = await _pageviewStatisticsClient.GetFromJsonAsync<IEnumerable<PageviewHourStatistics>>("/api/pageviewHourStatistics" + query);

            UpdateData(DateTime.Today.AddDays(-1), 0);//Yesterday
            UpdateData(DateTime.Today, 1);//Today

            void UpdateData(DateTime date, int day)
            {
                var statistics = _pageviewHourStatistics
                                .Where(statistic => statistic.Time.Date == date);

                for (int i = 0; i < 24; i++)
                {
                    if (_current == "PV")
                    {
                        _option.Series[day].Data[i] = statistics.FirstOrDefault(statistic => statistic.Time.Hour == i)?.PV ?? 0;
                    }
                    else if (_current == "UV")
                    {
                        _option.Series[day].Data[i] = statistics.FirstOrDefault(statistic => statistic.Time.Hour == i)?.UV ?? 0;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        private async Task UpdateOnlineUserCountAsync()
        {
            var content = await _userClient.GetStringAsync("/api/User/GetOnlineUserCount");
            _onlineUserCount = Convert.ToInt32(content);
        }

        private async Task HandleOnChangeAsync(StringNumber value)
        {
            _current = value;
            await UpdateVisitPageHourStatisticsAsync();
        }
    }
}
