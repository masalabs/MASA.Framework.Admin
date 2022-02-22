using MASA.Framework.Admin.Contracts.PageviewStatistics;
using MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageviewDayStatisticsController : ControllerBase
    {
        private readonly IPageviewDayStatisticsRepository _repository;

        public PageviewDayStatisticsController(IPageviewDayStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PageviewDayStatisticsQueryViewModel viewModel)
        {
            var statistics = _repository.GetAll();

            if (viewModel.StartDate != null)
            {
                statistics = statistics.Where(statistic => statistic.Date >= viewModel.StartDate);
            }

            if (viewModel.EndDate != null)
            {
                statistics = statistics.Where(statistic => statistic.Date <= viewModel.EndDate);
            }

            return Ok(statistics);
        }
    }
}
