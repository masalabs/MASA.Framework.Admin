using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands;
using MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries;

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
            App.MapGet("api/dicValue/get/{id}", GetAsync);
            App.MapPost("api/dicValue/getPage", GetPageAsync);
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
        public async Task<IResult> GetAsync([FromRoute] Guid id, [FromServices] IEventBus eventBus)
        {
            try
            {
                var query = new DicValueQuery(id);
                await eventBus.PublishAsync(query);
                return Results.Ok(query.Result);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        public async Task<IResult> GetPageAsync([FromBody] DicValuePagingOptions options, [FromServices] IEventBus eventBus)
        {
            try
            {
                var query = new DicValuePageQuery(options);
                await eventBus.PublishAsync(query);
                return Results.Ok(query.Result);
            }
            catch
            {
                return Results.BadRequest();
            }
        }
    }
}
