namespace MASA.Framework.Admin.Service.Authentication.Domain.Services;

public class PermissionDomainService : DomainService
{
    public PermissionDomainService(IDomainEventBus eventBus) : base(eventBus)
    {
    }

    public async Task AddRolePermissionAsync(
        Permission permission,
        Guid? roleId,
        PermissionType permissionType)
    {
        if (roleId != null)
        {
            var command = new AddRolePermissionDomainCommand(
                permission.Modifier,
                permission.Id,
                roleId.Value);
            await EventBus.Enqueue(command);
            await EventBus.Enqueue(new AddRolePermissionDomainEvent(
                roleId.Value,
                (int)permission.ObjectType,
                permission.Name,
                permission.Resource,
                permission.Scope,
                permission.Action,
                permission.Id,
                (int)permissionType));
            await EventBus.PublishQueueAsync();
        }
    }
}
