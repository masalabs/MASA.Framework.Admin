namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions;

public class PermissionsCommandHandler
{
    private readonly IPermissionRepository _repository;
    private readonly PermissionDomainService _domainService;

    public PermissionsCommandHandler(IPermissionRepository repository, PermissionDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    [EventHandler]
    public async Task AddAsync(AddPermissionCommand command)
    {
        if (await _repository.AnyAsync(permission =>
                permission.ObjectType == command.ObjectType &&
                permission.Name == command.Name &&
                permission.Resource == command.Resource &&
                permission.Scope == command.Scope &&
                permission.Action == command.Action))
            throw new UserFriendlyException("The current permission already exists");

        var permission = new Permission(
            command.Creator,
            command.ObjectType,
            command.Name,
            command.Resource,
            command.Scope,
            command.Action,
            command.PermissionType);
        await _repository.AddAsync(permission);
        await _domainService.AddRolePermissionAsync(permission, command.RoleId, command.PermissionType, command.PermissionEffect);
    }

    [EventHandler]
    public async Task EditAsync(EditPermissionCommand command)
    {
        var permission = await _repository.FindAsync(command.PermissionId);
        if (permission == null)
            throw new UserFriendlyException("The current permission does not exist");

        permission.Update(command.Creator, command.Name);
        await _repository.UpdateAsync(permission);
    }

    [EventHandler]
    public async Task ChangeStateAsync(ChangeStateCommand command)
    {
        var permission = await _repository.FindAsync(command.PermissionId);
        if (permission == null)
            throw new UserFriendlyException("The current permission does not exist");

        permission.ChangeState(command.Creator, command.Enable);
        await _repository.UpdateAsync(permission);
    }
}
