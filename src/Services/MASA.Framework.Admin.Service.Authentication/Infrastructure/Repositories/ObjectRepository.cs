namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Repositories;

public class ObjectRepository :
    Repository<AuthenticationDbContext, Domain.Aggregate.ObjectAggregate.Object>,
    IObjectRepository
{
    public ObjectRepository(AuthenticationDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<bool> ExistAsync(string code)
    {
        return _context.Set<Domain.Aggregate.ObjectAggregate.Object>().AnyAsync(obj => obj.Code == code);
    }

    public async Task<Domain.Aggregate.ObjectAggregate.Object?> FindAsync(Guid id)
    {
        return await _context.Set<Domain.Aggregate.ObjectAggregate.Object>()
            .Include(obj => obj.Permissions)
            .Where(obj => obj.Id == id)
            .FirstOrDefaultAsync();
    }

    public async new Task<IEnumerable<Domain.Aggregate.ObjectAggregate.Object>> GetListAsync(
        Expression<Func<Domain.Aggregate.ObjectAggregate.Object, bool>> predicate,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _context.Set<Domain.Aggregate.ObjectAggregate.Object>()
            .Include(obj => obj.Permissions)
            .ToListAsync(cancellationToken);
    }
}
