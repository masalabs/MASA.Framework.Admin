using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Repositories
{
    public class VisitPageDayStatisticsRepository : IVisitPageDayStatisticsRepository
    {
        private readonly AdminDbContext _dbContext;

        public VisitPageDayStatisticsRepository(AdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<VisitPageDayStatistics> GetAll()
        {
            return _dbContext.VisitPageDayStatistics.AsQueryable();
        }
    }
}
