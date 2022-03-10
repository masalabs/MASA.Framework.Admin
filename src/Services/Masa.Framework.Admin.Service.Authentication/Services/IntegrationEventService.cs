namespace Masa.Framework.Admin.Service.Authentication.Services;

public class IntegrationEventService : ServiceBase
{
    private const string DAPR_PUBSUB_NAME = "pubsub";

    public IntegrationEventService(
        IServiceCollection services)
        : base(services)
    {
        App.MapPost(Routing.RolePermission, AddRolePermissionAsync);
    }

    [Topic(DAPR_PUBSUB_NAME, nameof(AddRoleAsync))]
    public async Task AddRoleAsync(
        AddRoleIntegraionEvent integraionEvent,
        [FromServices] IEventBus eventBus)
    {
        await eventBus.PublishAsync(new RefreshRoleCommand(integraionEvent.RoleId));
    }

    /// <summary>
    /// Subscribe to role permission update messages and update a role's permissions（No inheritance license）
    /// </summary>
    /// <param name="integrationEvent"></param>
    /// <param name="eventBus"></param>
    [Topic(DAPR_PUBSUB_NAME, nameof(AddRolePermissionIntegraionEvent))]
    public async Task AddRolePermissionAsync(
        AddRolePermissionIntegraionEvent integrationEvent,
        [FromServices] IEventBus eventBus)
    {
        await eventBus.PublishAsync(new RefreshRolePermissionCommand(integrationEvent.RoleId));
    }
}
