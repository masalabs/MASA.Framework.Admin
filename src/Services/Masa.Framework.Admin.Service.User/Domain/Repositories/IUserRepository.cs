namespace Masa.Framework.Admin.Service.User.Domain.Repositories;

public interface IUserRepository : IRepository<Aggregates.User, Guid>
{
    Task<Aggregates.User> GetByIdAsync(Guid Id);

    Task<int> GetUserCountAsync(Expression<Func<Aggregates.User, bool>> predicate);

    Task<List<Aggregates.User>> QueryListAsync(Expression<Func<Aggregates.User, bool>> predicate, params string[] includProperties);
}

