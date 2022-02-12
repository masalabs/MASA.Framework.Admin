namespace MASA.Framework.Admin.Service.Authentication.Domain.Repositories;

public interface IObjectRepository : IRepository<Aggregate.ObjectAggregate.Object>
{
    Task<bool> ExistAsync(string code);

    Task<Aggregate.ObjectAggregate.Object?> FindAsync(Guid id);
}
