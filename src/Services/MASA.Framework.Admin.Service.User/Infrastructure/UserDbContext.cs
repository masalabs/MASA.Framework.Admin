using MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations;
using MASA.Utils.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.User.Infrastructure;

public class UserDbContext : IntegrationEventLogContext
{
    public const string DEFAULT_SCHEMA = "user";

    public DbSet<Users> Users { get; set; } = null!;

    public UserDbContext(MasaDbContextOptions<UserDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}

