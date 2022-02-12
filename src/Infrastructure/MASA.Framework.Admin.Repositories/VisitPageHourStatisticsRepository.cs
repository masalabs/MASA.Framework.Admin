using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Repositories
{
    public class VisitPageHourStatisticsRepository : IVisitPageHourStatisticsRepository
    {
        private readonly AdminDbContext _dbContext;

        public VisitPageHourStatisticsRepository(AdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<VisitPageHourStatistics> GetAll()
        {
            return _dbContext.VisitPageHourStatistics.AsQueryable();
        }
    }
}
