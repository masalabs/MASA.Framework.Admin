using MASA.Framework.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Repositories
{
    public interface IVisitPageHourStatisticsRepository
    {
        IQueryable<VisitPageHourStatistics> GetAll();
    }
}
