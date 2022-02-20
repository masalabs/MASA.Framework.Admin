using MASA.Framework.Admin.Contracts.PageviewStatistics;
using MASA.Framework.Admin.Service.Logging.Infrastructure;
using MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories
{
    public class PageviewDayStatisticsRepository : IPageviewDayStatisticsRepository
    {
        private readonly PageviewStatisticsDbContext _dbContext;

        public PageviewDayStatisticsRepository(PageviewStatisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<PageviewDayStatistics> GetAll()
        {
            return _dbContext.PageviewDayStatistics.AsQueryable();
        }
    }
}
