namespace Masa.Framework.Admin.Service.User.Domain.Repositories;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByIdAsync(Guid Id);
}

