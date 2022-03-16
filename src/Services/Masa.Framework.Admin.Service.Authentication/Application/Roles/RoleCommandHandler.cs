namespace Masa.Framework.Admin.Service.Authentication.Application.Roles;

public class RoleCommandHandler
{
    private readonly IRoleRepository _repository;
    private readonly IDistributedCacheClient _distributedCacheClient;
    private readonly RoleDomainService _domainService;
    private readonly IEventBus _eventBus;
    private readonly DbContext _dbContext;

    public RoleCommandHandler(
        IRoleRepository repository,
        IDistributedCacheClient distributedCacheClient,
        RoleDomainService domainService,
        IEventBus eventBus,
        AuthenticationDbContext dbContext)
    {
        _repository = repository;
        _distributedCacheClient = distributedCacheClient;
        _domainService = domainService;
        _eventBus = eventBus;
        _dbContext = dbContext;
    }

    [EventHandler]
    public async Task AddAsync(AddRoleCommand command)
    {
        if (await _repository.GetCountAsync(role => role.Name == command.Name) > 0)
            throw new UserFriendlyException("The current role already exists");

        var role = new Role(command.Creator, command.Name, command.Number, command.Description);
        role.SetInheritedRole(command.ChildrenRoleIds);
        await _repository.AddAsync(role);
        await _domainService.AddRoleAsync(role);
    }

    [EventHandler]
    public async Task AddChildRolesAsync(AddChildrenRolesCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");
        role.SetInheritedRole(command.ChildrenRoleIds);
        await _repository.UpdateAsync(role);
    }

    [EventHandler]
    public async Task EditAsync(EditRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.Update(command.Creator, command.Name, command.Description);
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
        await _repository.UpdateAsync(role);
    }

    [EventHandler]
    public async Task DeleteRolePermissionAsync(DeleteRolePermissionCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.DeleteRolePermission(command.Creator, command.PermissionId);
        await _repository.UpdateAsync(role);
        await _eventBus.PublishAsync(new DeletePermissionCommand() { PermissionId = command.PermissionId });
    }
}
