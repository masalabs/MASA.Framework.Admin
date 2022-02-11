﻿using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands;
using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries;

namespace MASA.Framework.Admin.Service.Dictionary.Service
{
    public class DicService : ServiceBase
    {
        private readonly IEventBus _eventBus;

        public DicService(IServiceCollection services) : base(services)
        {
            _eventBus = this.GetService<IEventBus>() ?? throw new ArgumentNullException(nameof(IEventBus));

            App.MapPost("/api/dic/create", CreateAsync);
            App.MapPost("/api/dic/update", UpdateAsync);
            App.MapPost("/api/dic/delete/{id}", DeleteAsync);
            App.MapPost("/api/dic/deleteAll", DeleteAllAsync);
            App.MapGet("api/dic/get/{id}", GetAsync);
            App.MapGet("api/dic/getPage", GetPageAsync);
        }

        public async Task<IResult> CreateAsync([FromBody] Dic dic, [FromServices] IEventBus eventBus)
        {
            try
            {
                dic.Id = Guid.NewGuid();
                var addCoommand = new AddDicCommand(dic);
                await eventBus.PublishAsync(addCoommand);
                return Results.Ok(dic.Id);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        public async Task<IResult> UpdateAsync([FromBody] Dic dic, [FromServices] IEventBus eventBus)
        {
            try
            {
                var updateCommand = new UpdateDicCommand(dic);
                await eventBus.PublishAsync(updateCommand);
                return Results.Ok(dic.Id);
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
                var deleteCommand = new DeleteDicCommand(id);
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
                var deleteCommand = new DeleteAllDicCommand(ids);
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
                var query = new DicQuery(id);
                await eventBus.PublishAsync(query);
                return Results.Ok(query.Result);
            }
            catch
            {
                return Results.BadRequest();
            }
        }

        public async Task<IResult> GetPageAsync([FromBody] DicPagingOptions options, [FromServices] IEventBus eventBus)
        {
            try
            {
                var query = new DicPageQuery(options);
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