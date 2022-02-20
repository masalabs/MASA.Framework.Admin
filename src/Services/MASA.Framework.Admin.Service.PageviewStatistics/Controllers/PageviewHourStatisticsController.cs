using MASA.Framework.Admin.Contracts.PageviewStatistics;
using MASA.Framework.Admin.Repositories;
using MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageviewHourStatisticsController : ControllerBase
    {
        private readonly IPageviewHourStatisticsRepository _repository;

        public PageviewHourStatisticsController(IPageviewHourStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PageviewHourStatisticsQueryViewModel viewModel)
        {
            var statistics = _repository.GetAll();

            if (viewModel.StartTime != null)
            {
                statistics = statistics.Where(statistic => statistic.Time >= viewModel.StartTime);
            }

            if (viewModel.EndTime != null)
            {
                statistics = statistics.Where(statistic => statistic.Time <= viewModel.EndTime);
            }

            return Ok(statistics);
        }
    }
}
