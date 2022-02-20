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
        var rule = await _cacheClient.GetAsync<RoleInfo>(string.Format(CacheConst.Cache.Role, query.RoleId));
        if (rule != null)
            query.Result = rule;
        else
        {
            var roleBaseQuery = new RoleBaseQuery(query.RoleId);
            await _eventBus.PublishAsync(roleBaseQuery);
            await _cacheClient.SetAsync(string.Format(CacheConst.Cache.Role, query.RoleId), query.Result);
        }
    }
}
