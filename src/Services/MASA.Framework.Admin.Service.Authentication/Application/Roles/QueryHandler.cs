namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class QueryHandler
{
    private readonly IRoleRepository _repository;

    public QueryHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task RolesQueryAsync(RolesQuery query)
    {

    }
}
