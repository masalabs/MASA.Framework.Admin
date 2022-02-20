namespace MASA.Framework.Admin.Service.User.Domain.Repositories;

public interface IUserRepository : IRepository<Aggregates.User>
{
    Task<Domain.Aggregates.User?> GetByIdAsync(Guid Id);
}

