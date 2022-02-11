namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class QueryHandler
{
    private readonly IRoleRepository _repository;

    public QueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public Task RolesQueryAsync(RolesQuery query)
    {
        var list = _repository.GetList();
        return Task.CompletedTask;
    }
}
