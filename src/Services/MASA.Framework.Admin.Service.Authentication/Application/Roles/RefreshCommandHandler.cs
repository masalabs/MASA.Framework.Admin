namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class RefreshCommandHandler
{
    private readonly IRoleRepository _repository;
    private readonly IDistributedCacheClient _cacheClient;
    private readonly IEventBus _eventBus;

    public RefreshCommandHandler(
        IRoleRepository repository,
        IDistributedCacheClient cacheClient,
        IEventBus eventBus)
    {
        _repository = repository;
        _cacheClient = cacheClient;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task RefreshRoleAsync(RefreshRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        var query = new RoleBaseQuery(command.RoleId);
        await _eventBus.PublishAsync(query);
        await _cacheClient.SetAsync(string.Format(CacheConst.Cache.Role, command.RoleId), query.Result);
    }

    [EventHandler]
    public async Task RefreshRolePermissionAsync(RefreshRolePermissionCommand command)
    {
        var query = new RolePermissionQuery(command.RoleId);
        await _eventBus.PublishAsync(query);
        await _cacheClient.SetAsync(string.Format(CacheConst.Cache.RolePermission, command.RoleId), query.Result);
    }
}
