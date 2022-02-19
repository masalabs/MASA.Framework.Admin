namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserRepository : Repository<UserDbContext, Domain.Aggregate.User>, IUserRepository
{
    public UserRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Domain.Aggregate.User?> GetByIdAsync(Guid Id)
    {
        return await _context.Set<Domain.Aggregate.User>().Where(a => a.Id == Id)
            .Include(b => b.UserRoles).SingleOrDefaultAsync();
    }
}

