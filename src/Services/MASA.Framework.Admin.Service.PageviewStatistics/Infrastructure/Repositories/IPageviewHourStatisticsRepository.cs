using MASA.Framework.Admin.Contracts.PageviewStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.PageviewStatistics.Infrastructure.Repositories
{
    public interface IPageviewHourStatisticsRepository
    {
        IQueryable<PageviewHourStatistics> GetAll();
    }
}
