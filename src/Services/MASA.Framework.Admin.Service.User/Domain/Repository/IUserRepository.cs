namespace MASA.Framework.Admin.Service.User.Domain.Repository;

public interface IUserRepository : IRepository<Aggregate.User>
{
    Task<Domain.Aggregate.User?> GetByIdAsync(Guid Id);
}

