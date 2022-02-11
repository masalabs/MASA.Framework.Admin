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
        modelBuilder.ApplyConfiguration(new ObjectEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ObjectItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RoleItemEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new IntegrationEventLogEntityTypeConfiguration());
    }
}
