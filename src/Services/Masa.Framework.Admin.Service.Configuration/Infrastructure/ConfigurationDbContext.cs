namespace Masa.Framework.Admin.Configuration.Infrastructure;

public class ConfigurationDbContext : IntegrationEventLogContext
{
    public const string DEFAULT_SCHEMA = "configuration";

    public ConfigurationDbContext(MasaDbContextOptions<ConfigurationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MenuEntityTypeConfiguration());
        base.OnModelCreatingExecuting(modelBuilder);
    }
}
