using MASA.Framework.Admin.Models;
using MASA.Framework.Admin.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitPageHourStatisticsController : ControllerBase
    {
        private readonly IVisitPageHourStatisticsRepository _repository;

        public VisitPageHourStatisticsController(IVisitPageHourStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] VisitPageHourStatisticsQueryViewModel viewModel)
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
