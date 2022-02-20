namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserRepository : Repository<UserDbContext, Domain.Aggregates.User>, IUserRepository
{
    UserDbContext _context;

    public UserRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
        _context = context;
    }

    public async Task<Domain.Aggregates.User?> GetByIdAsync(Guid Id)
    {
        return await _context.Set<Domain.Aggregates.User>().Where(a => a.Id == Id)
            .Include(b => b.UserRoles).SingleOrDefaultAsync();
    }
}

