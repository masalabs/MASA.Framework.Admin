using MASA.Framework.Admin.Contracts.Logging;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Logging.Infrastructure
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperationLog> OperationLogs { get; set; }
    }
}
