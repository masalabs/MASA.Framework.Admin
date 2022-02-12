using MASA.Framework.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Repositories
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options)
            : base(options)
        {
        }

        public DbSet<OperationLog> OperationLogs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<VisitPageDayStatistics> VisitPageDayStatistics { get; set; }

        public DbSet<VisitPageHourStatistics> VisitPageHourStatistics { get; set; }
    }
}
