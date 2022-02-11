namespace MASA.Framework.Admin.Services.BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JobController : ControllerBase
    {
        [HttpGet("/query")]
        public async Task<ActionResult<IEnumerable<Job>>> Query([FromServices] IEventBus eventBus)
        {
            var query = new JobQuery();
            await eventBus.PublishAsync(query);
            return Ok(query.Result);
        }

    }
}