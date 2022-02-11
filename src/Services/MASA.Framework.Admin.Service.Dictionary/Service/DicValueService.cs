using MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands;

namespace MASA.Framework.Admin.Service.Dictionary.Service
{
    public class DicValueService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public DicValueService(IServiceCollection services) : base(services)
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            App.MapPost("/api/dicValue/create", CreateAsync);
            App.MapPost("/api/dicValue/update", UpdateAsync);
            App.MapPost("/api/dicValue/delete/{id}", DeleteAsync);
            App.MapPost("/api/dicValue/deleteAll", DeleteAllAsync);
        }

        public async Task<IResult> CreateAsync([FromBody] DicValue dicValue, [FromServices] IEventBus eventBus)
        {
            try
            {
                dicValue.Id = Guid.NewGuid();
                var addCoommand = new AddDicValueCommand(dicValue);
                await eventBus.PublishAsync(addCoommand);
                return Results.Ok(dicValue.Id);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        public async Task<IResult> UpdateAsync([FromBody] DicValue dicValue, [FromServices] IEventBus eventBus)
        {
            try
            {
                var updateCommand = new UpdateDicValueCommand(dicValue);
                await eventBus.PublishAsync(updateCommand);
                return Results.Ok(dicValue.Id);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        public async Task<IResult> DeleteAsync([FromRoute] Guid id, [FromServices] IEventBus eventBus)
        {
            try
            {
                var deleteCommand = new DeleteDicValueCommand(id);
                await eventBus.PublishAsync(deleteCommand);
                return Results.Ok();
            }
            catch
            {
                return Results.BadRequest();
            }
        }
        public async Task<IResult> DeleteAllAsync(List<Guid> ids, [FromServices] IEventBus eventBus)
        {
            try
            {
                var deleteCommand = new DeleteAllDicValueCommand(ids);
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
