namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions;

public class PermissionsCommandHandler
{
    private readonly IPermissionRepository _repository;
    private readonly PermissionDomainService _domainService;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionsCommandHandler(
        IPermissionRepository repository,
        PermissionDomainService domainService,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _domainService = domainService;
        _unitOfWork = unitOfWork;
    }

    [EventHandler]
    public async Task AddRolePermissionAsync(AddPermissionCommand command)
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
            command.PermissionId,
            command.ObjectType,
            command.Name,
            command.Resource,
            command.Scope,
            command.Action,
            command.PermissionType);
        await _repository.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();
        if (command.RoleId.HasValue)
        {
            await _domainService.AddRolePermissionAsync(permission, command.RoleId, command.PermissionType);
        }
        if (command.UserGroupId.HasValue)
        {
            await _domainService.AddGroupPermissionAsync(permission.Id, command.UserGroupId);
        }
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
    public async Task DeleteAsync(DeletePermissionCommand command)
    {
        var permission = await _repository.FindAsync(command.PermissionId);
        if (permission == null)
            throw new UserFriendlyException("The current permission does not exist");

        await _repository.RemoveAsync(permission);
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
