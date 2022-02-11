namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure
{
    public class JobDbContext : IntegrationEventLogContext
    {
        public DbSet<Job> Orders { get; set; } = null!;

        public JobDbContext(MasaDbContextOptions<JobDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreatingExecuting(ModelBuilder builder)
        {
            base.OnModelCreatingExecuting(builder);
        }
    }
}