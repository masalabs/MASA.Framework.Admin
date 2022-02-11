namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure
{
    public class DictionaryDbContext : IntegrationEventLogContext
    {
        public DbSet<Dic> Dics { get; set; } = null!;

        public DbSet<DicValue> DicValues { get; set; } = null!;

        public DbSet<Module> Modules { get; set; } = null!;

        public DictionaryDbContext(MasaDbContextOptions<DictionaryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreatingExecuting(ModelBuilder builder)
        {
            base.OnModelCreatingExecuting(builder);
        }
    }
}
