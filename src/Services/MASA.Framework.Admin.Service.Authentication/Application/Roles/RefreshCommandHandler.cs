using MASA.Framework.Admin.Service.Authentication.Infrastructure.Cache;

namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class RefreshCommandHandler
{
    private readonly IRoleRepository _repository;
    private readonly IDistributedCacheClient _distributedCacheClient;
    private readonly IEventBus _eventBus;

    public RefreshCommandHandler(
        IRoleRepository repository,
        IDistributedCacheClient distributedCacheClient,
        IEventBus eventBus)
    {
        _repository = repository;
        _distributedCacheClient = distributedCacheClient;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task RefreshRoleAsync(RefreshRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        ArgumentNullException.ThrowIfNull(role, nameof(role));

        await _distributedCacheClient.SetAsync(string.Format(CacheConst.Cache.Role, command.RoleId), new RoleInfo()
        {
            Name = role.Name,
            Describe = role.Describe,
            Number = role.Number,
            Enable = role.Enable,
            ChildrenRoleIds = role.RoleItems.Select(roleItem => roleItem.RoleId).ToList()
        });
    }

    [EventHandler]
    public async Task RefreshRolePermissionAsync(RefreshRolePermissionCommand command)
    {
        var query = new RolePermissionQuery(command.RoleId);
        await _eventBus.PublishAsync(query);
        await _distributedCacheClient.SetAsync(string.Format(CacheConst.Cache.RolePermission, command.RoleId), query.Result);
    }
}
