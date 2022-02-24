using MASA.Utils.Data.EntityFrameworkCore;
using System.Reflection;

namespace MASA.Framework.Admin.Service.LogStatistics.Infrastructure
{
    public class LogStatisticsDbContext : IntegrationEventLogContext
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
