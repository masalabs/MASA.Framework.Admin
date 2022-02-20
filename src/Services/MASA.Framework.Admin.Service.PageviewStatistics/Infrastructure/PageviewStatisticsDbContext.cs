using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Contracts.PageviewStatistics;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Logging.Infrastructure
{
    public class PageviewStatisticsDbContext : DbContext
    {
        public PageviewStatisticsDbContext(DbContextOptions<PageviewStatisticsDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperationLog> OperationLogs { get; set; }

        public DbSet<PageviewDayStatistics> PageviewDayStatistics { get; set; }

        public DbSet<PageviewHourStatistics> PageviewHourStatistics { get; set; }
    }
}
