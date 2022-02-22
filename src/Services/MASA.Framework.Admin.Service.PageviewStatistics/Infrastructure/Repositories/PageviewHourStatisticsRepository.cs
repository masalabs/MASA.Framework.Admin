using MASA.Framework.Admin.Contracts.PageviewStatistics;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories
{
    public class PageviewHourStatisticsRepository : IPageviewHourStatisticsRepository
    {
        private readonly PageviewStatisticsDbContext _dbContext;

        public PageviewHourStatisticsRepository(PageviewStatisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<PageviewHourStatistics> GetAll()
        {
            return _dbContext.PageviewHourStatistics.AsQueryable();
        }
    }
}
