using Masa.Framework.Sdks.Authentication.Response.LogStatistics;

namespace Masa.Framework.Admin.Web.Pages.Dashboard
{
    public partial class Index
    {

        private IEnumerable<OperationLogItemResponse> _operationLogs;
        private int _onlineUserCount;
        private IEnumerable<StatisticsQueryResponse> _pageviewDayStatistics;
        private IEnumerable<StatisticsQueryResponse> _pageviewHourStatistics;
        private StringNumber _current = "PV";
        private List<DataTableHeader<OperationLogItemResponse>> _headers = new List<DataTableHeader<OperationLogItemResponse>>
        {
           new ()
           {
                Text= "用户名",
                Align= "start",
                Sortable= false,
                Value= nameof(OperationLogItemResponse.UserName),
                CellClass="text-subtitle"
          },
          new ()
          {
                Text= "描述",
                Value= nameof(OperationLogItemResponse.Description),
                CellClass="text-subtitle"
          },
          new ()
          {
                Text= "时间",
                Value= nameof(OperationLogItemResponse.CreateTime),
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

        protected StatisticsQueryResponse TodayStatistics => _pageviewDayStatistics.FirstOrDefault(statistic => statistic.DateTime == DateTime.Today);

        protected StatisticsQueryResponse YesterdayStatistics => _pageviewDayStatistics.FirstOrDefault(statistic => statistic.DateTime == DateTime.Today.AddDays(-1));

        [Inject]
        public LogStatisticsCaller LogStatisticsCaller { get; set; } = null!;

        [Inject]
        public UserCaller UserCaller { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            await UpdateOperationLogsAsync(0, int.MaxValue);
            await UpdateVisitPageDayStatisticsAsync();
            await UpdateVisitPageHourStatisticsAsync();
            await UpdateOnlineUserCountAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            var res = await LogStatisticsCaller.CreateLogAsync(new OperationLogCreateRequest
            {
                Description = "访问了Dashboard页面",
                OperationLogType = OperationLogType.VisitPage
            });
        }

        private async Task UpdateOperationLogsAsync(int offset = 0, int limit = 10)
        {
            var res = await LogStatisticsCaller.GetLogListAsync(offset + 1, limit);
            if (res.Success)
            {
                _operationLogs = res.Data.Items;
            }
        }

        private async Task UpdateVisitPageDayStatisticsAsync()
        {
            var startDate = DateTime.Today.AddDays(-1);
            var endDate = DateTime.Today;

            var res = await LogStatisticsCaller.GetDayStatisticsAsync(startDate, endDate);
            if (res.Success)
            {
                _pageviewDayStatistics = res.Data;
            }
        }

        private async Task UpdateVisitPageHourStatisticsAsync()
        {
            var startTime = DateTime.Today.AddDays(-1);
            var endTime = DateTime.Today.AddDays(1);

            var res = await LogStatisticsCaller.GetHourStatisticsAsync(startTime, endTime);

            if (res.Success)
            {
                _pageviewHourStatistics = res.Data;

                UpdateData(DateTime.Today.AddDays(-1), 0);//Yesterday
                UpdateData(DateTime.Today, 1);//Today
            }

            void UpdateData(DateTime date, int day)
            {
                var statistics = _pageviewHourStatistics
                                .Where(statistic => statistic.DateTime.Date == date);

                for (int i = 0; i < 24; i++)
                {
                    if (_current == "PV")
                    {
                        _option.Series[day].Data[i] = statistics.FirstOrDefault(statistic => statistic.DateTime.Hour == i)?.PV ?? 0;
                    }
                    else if (_current == "UV")
                    {
                        _option.Series[day].Data[i] = statistics.FirstOrDefault(statistic => statistic.DateTime.Hour == i)?.UV ?? 0;
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
            var res = await UserCaller.GetUserStatisticAsync();
            if (res.Success)
            {
                _onlineUserCount = res.Data.UserOnlineCount;
            }
        }

        private async Task HandleOnChangeAsync(StringNumber value)
        {
            _current = value;
            await UpdateVisitPageHourStatisticsAsync();
        }
    }
}
