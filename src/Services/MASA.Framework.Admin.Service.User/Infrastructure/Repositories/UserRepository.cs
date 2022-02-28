using System.Linq.Expressions;

namespace Masa.Framework.Admin.Service.User.Infrastructure.Repositories;

public class UserRepository : Repository<UserDbContext, Domain.Aggregates.User>, IUserRepository
{
    public UserRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Domain.Aggregates.User?> GetByIdAsync(Guid Id)
    {
        return await _context.Set<Domain.Aggregates.User>().Where(a => a.Id == Id)
            .Include(b => b.UserRoles).FirstOrDefaultAsync();
    }

    public async Task<int> GetUserCountAsync(Expression<Func<Domain.Aggregates.User, bool>> predicate)
    {
        return await _context.Set<Domain.Aggregates.User>().Where(predicate).CountAsync();
    }

    public async Task<List<Domain.Aggregates.User>> GetUsersByDepartment(Guid departmentId, int pageIndex, int pageSize)
    {
        return await _context.Set<DepartmentUser>().Where(a => a.Department.Id == departmentId)
            .Select(du => du.UserId).Join(_context.Set<Domain.Aggregates.User>(),
            userId => userId, user => user.Id, (userId, user) => user)
            .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}

