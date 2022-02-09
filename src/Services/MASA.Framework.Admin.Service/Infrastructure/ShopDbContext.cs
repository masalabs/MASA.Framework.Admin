namespace MASA.Framework.Admin.Service.Infrastructure
{
    public class ShopDbContext : IntegrationEventLogContext
    {
        public DbSet<Order> Orders { get; set; } = null!;

        public ShopDbContext(MasaDbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreatingExecuting(ModelBuilder builder)
        {
            base.OnModelCreatingExecuting(builder);
        }
    }
}