namespace MASA.Framework.Admin.Service.Authentication.Infrastructure;

public class AuthenticationDbContext : IntegrationEventLogContext
{
    public const string DEFAULT_SCHEMA = "authentication";

    public AuthenticationDbContext(MasaDbContextOptions<AuthenticationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionEntityTypeConfiguration());
        base.OnModelCreatingExecuting(modelBuilder);
    }
}
