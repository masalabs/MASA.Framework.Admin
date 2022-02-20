namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class RoleCommandHandler
{
    private readonly IRoleRepository _repository;
    private readonly IDistributedCacheClient _distributedCacheClient;
    private readonly RoleDomainService _domainService;
    private readonly IEventBus _eventBus;

    public RoleCommandHandler(
        IRoleRepository repository,
        IDistributedCacheClient distributedCacheClient,
        RoleDomainService domainService,
        IEventBus eventBus)
    {
        _repository = repository;
        _distributedCacheClient = distributedCacheClient;
        _domainService = domainService;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task AddAsync(AddRoleCommand command)
    {
        if (await _repository.AnyAsync(role => role.Name == command.Name))
            throw new UserFriendlyException("The current role already exists");

        var role = new Role(command.Creator, command.Name, command.Number);
        role.SetInheritedRole(command.ChildrenRoleIds);
        await _repository.AddAsync(role);
        await _domainService.AddRoleAsync(role);
    }

    [EventHandler]
    public async Task EditAsync(EditRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.Update(command.Creator, command.Name, command.Describe);
        await _repository.UpdateAsync(role);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        await _repository.RemoveAsync(role);
    }

    [EventHandler]
    public async Task AddRolePermissionAsync(AddRolePermissionDomainCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.AddRolePermission(command.Creator, command.PermissionId);
    }

    [EventHandler]
    public async Task DeleteRolePermissionAsync(DeleteRolePermissionCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.DeleteRolePermission(command.Creator, command.PermissionId);
    }
}
