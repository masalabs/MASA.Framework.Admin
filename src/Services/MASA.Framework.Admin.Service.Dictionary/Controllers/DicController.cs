using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.Dictionary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicController : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> Add([FromBody] Dic dic, [FromServices] IEventBus eventBus)
        {
            try
            {
                dic.Id = Guid.NewGuid();
                var addCoommand = new AddCommand(dic);
                await eventBus.PublishAsync(addCoommand);
                return Results.Ok(dic.Id);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        [HttpPost]
        public async Task<IResult> Update([FromBody] Dic dic, [FromServices] IEventBus eventBus)
        {
            try
            {
                var updateCommand = new UpdateCommand(dic);
                await eventBus.PublishAsync(updateCommand);
                return Results.Ok(dic.Id);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        [HttpPost]
        public async Task<IResult> Delete([FromRoute] Guid id, [FromServices] IEventBus eventBus)
        {
            try
            {
                var deleteCommand = new DeleteCommand(id);
                await eventBus.PublishAsync(deleteCommand);
                return Results.Ok();
            }
            catch
            {
                return Results.BadRequest();
            }
        }
    }
}
