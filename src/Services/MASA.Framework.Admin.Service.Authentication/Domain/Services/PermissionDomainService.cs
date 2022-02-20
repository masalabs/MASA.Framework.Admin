namespace MASA.Framework.Admin.Service.Authentication.Domain.Services;

public class PermissionDomainService : DomainService
{
    public PermissionDomainService(IDomainEventBus eventBus) : base(eventBus)
    {
    }

    public async Task AddRolePermissionAsync(
        Permission permission,
        Guid? roleId,
        PermissionType permissionType,
        PermissionEffect? permissionEffect)
    {
        if (roleId != null)
        {
            var command = new AddRolePermissionDomainCommand(
                permission.Modifier,
                permission.Id,
                roleId.Value,
                (int)permissionType,
                (int)permissionEffect!.Value);
            await EventBus.PublishAsync(command);
        }
    }
}
