namespace MASA.Framework.Admin.Service.Dictionary
{
    public class DicDbContext : IntegrationEventLogContext
    {
        public DbSet<Dic> Dics { get; set; } = null!;

        public DicDbContext(MasaDbContextOptions<DicDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreatingExecuting(ModelBuilder builder)
        {
            base.OnModelCreatingExecuting(builder);
        }
    }
}
