using System.Reflection;

namespace MASA.Framework.Admin.Service.User.Infrastructure;

public class UserDbContext : IntegrationEventLogContext
{
    public const string DEFAULT_SCHEMA = "user";

    public UserDbContext(MasaDbContextOptions<UserDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        base.OnModelCreatingExecuting(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
