namespace Masa.Framework.Admin.Service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        [HttpGet("/query")]
        public async Task<ActionResult<IEnumerable<Order>>> Query([FromServices] IEventBus eventBus)
        {
            var query = new OrderQuery();
            await eventBus.PublishAsync(query);
            return Ok(query.Result);
        }

    }
}