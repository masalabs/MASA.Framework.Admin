namespace Masa.Framework.Admin.Service.User.Infrastructure.Repositories;

public class DepartmentRepository : Repository<UserDbContext, Department>, IDepartmentRepository
{
    public DepartmentRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Department?> GetByIdAsync(Guid Id)
    {
        return await _context.Set<Department>().Where(a => a.Id == Id)
        .Include(b => b.DepartmentUsers).FirstOrDefaultAsync();
    }
}

