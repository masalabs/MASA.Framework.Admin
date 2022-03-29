using Masa.Utils.Data.EntityFrameworkCore;

namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure
{
    public class LogStatisticsDbContext : MasaDbContext
    {
        public const string DEFAULT_SCHEMA = "log";

        public LogStatisticsDbContext(MasaDbContextOptions<LogStatisticsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
        {
            base.OnModelCreatingExecuting(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
