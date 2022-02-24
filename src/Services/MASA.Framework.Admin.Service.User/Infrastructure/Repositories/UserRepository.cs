using System.Linq.Expressions;

namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserRepository : Repository<UserDbContext, Domain.Aggregates.User>, IUserRepository
{
    public UserRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Domain.Aggregates.User?> GetByIdAsync(Guid Id)
    {
        return await _context.Set<Domain.Aggregates.User>().Where(a => a.Id == Id)
            .Include(b => b.UserRoles).SingleOrDefaultAsync();
    }

    public async Task<int> GetUserCountAsync(Expression<Func<Domain.Aggregates.User, bool>>? predicate = null)
    {
        return await _context.Set<Domain.Aggregates.User>().Where(predicate).CountAsync();
    }
}

