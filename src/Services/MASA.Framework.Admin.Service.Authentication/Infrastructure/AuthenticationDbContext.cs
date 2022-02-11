using MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.ObjectAggregate;
using MASA.Framework.Admin.Service.Authentication.Domain.Aggregate.RoleAggregate;

namespace MASA.Framework.Admin.Service.Authentication.Infrastructure;

public class AuthenticationDbContext : IntegrationEventLogContext
{
    public const string DEFAULT_SCHEMA = "authentication";

    public DbSet<Role> Roles { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<ObjectItem> Permission { get; set; }

    public AuthenticationDbContext(MasaDbContextOptions<AuthenticationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {

    }
}
