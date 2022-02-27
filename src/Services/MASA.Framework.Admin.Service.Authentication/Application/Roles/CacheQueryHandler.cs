namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class CacheQueryHandler
{
    private readonly IDistributedCacheClient _cacheClient;
    private readonly IEventBus _eventBus;

    public CacheQueryHandler(IDistributedCacheClient cacheClient,IEventBus eventBus)
    {
        _cacheClient = cacheClient;
        _eventBus = eventBus;
    }

    [EventHandler]
    public async Task GetRoleInfo(RoleCacheDetailQuery query)
    {
        var role = await _cacheClient.GetAsync<RoleInfo>(string.Format(CacheConst.Cache.Role, query.RoleId));
        if (role != null)
            query.Result = role;
        else
        {
            var roleBaseQuery = new RoleBaseQuery(query.RoleId);
            await _eventBus.PublishAsync(roleBaseQuery);
            await _cacheClient.SetAsync(string.Format(CacheConst.Cache.Role, query.RoleId), roleBaseQuery.Result);
            query.Result = roleBaseQuery.Result;
        }
    }
}
