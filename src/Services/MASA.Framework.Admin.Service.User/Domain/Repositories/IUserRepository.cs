using System.Linq.Expressions;

namespace MASA.Framework.Admin.Service.User.Domain.Repositories;

public interface IUserRepository : IRepository<Aggregates.User>
{
    Task<Aggregates.User?> GetByIdAsync(Guid Id);


    Task<int> GetUserCountAsync(Expression<Func<Aggregates.User, bool>>? predicate = null);
}

