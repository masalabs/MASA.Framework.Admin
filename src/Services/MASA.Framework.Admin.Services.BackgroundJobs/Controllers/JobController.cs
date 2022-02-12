namespace MASA.Framework.Admin.Services.BackgroundJobs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        [HttpPost("create")]
        public async Task Create(JobCreateCommand command,[FromServices] IEventBus eventBus)
        {
            await eventBus.PublishAsync(command);
        }

        [HttpPost("update")]
        public async Task Update(JobUpdateCommand command, [FromServices] IEventBus eventBus)
        {
            await eventBus.PublishAsync(command);
        }

        [HttpPost("page")]
        public async Task<ActionResult<PagingResult<JobViewModel>>> Page(JobQuery query, [FromServices] IEventBus eventBus)
        {
            await eventBus.PublishAsync(query);
            return Ok(query.Result);
        }

        [HttpPost("logpage")]
        public async Task<ActionResult<PagingResult<JobViewModel>>> Page(JobLogQuery query, [FromServices] IEventBus eventBus)
        {
            await eventBus.PublishAsync(query);
            return Ok(query.Result);
        }

    }
}