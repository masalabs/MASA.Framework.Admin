using MASA.Framework.Admin.Models;
using MASA.Framework.Admin.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitPageDayStatisticsController : ControllerBase
    {
        private readonly IVisitPageDayStatisticsRepository _repository;

        public VisitPageDayStatisticsController(IVisitPageDayStatisticsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] VisitPageDayStatisticsQueryViewModel viewModel)
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
