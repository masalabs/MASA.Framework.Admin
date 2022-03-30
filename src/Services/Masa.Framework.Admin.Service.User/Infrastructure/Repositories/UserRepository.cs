namespace Masa.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserRepository : Repository<UserDbContext, Domain.Aggregates.User, Guid>, IUserRepository
{
    public UserRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Domain.Aggregates.User> GetByIdAsync(Guid Id)
    {
        return await Context.Set<Domain.Aggregates.User>().Where(a => a.Id == Id)
            .Include(b => b.UserRoles).FirstAsync();
    }

    public async Task<int> GetUserCountAsync(Expression<Func<Domain.Aggregates.User, bool>> predicate)
    {
        return await Context.Set<Domain.Aggregates.User>().Where(predicate).CountAsync();
    }

    public async Task<List<Domain.Aggregates.User>> QueryListAsync(Expression<Func<Domain.Aggregates.User, bool>> predicate, params string[] includProperties)
    {
        var query = Context.Set<Domain.Aggregates.User>().Where(predicate);
        foreach (var includProperty in includProperties)
        {
            query = query.Include(includProperty);
        }
        return await query.ToListAsync();
    }
}

