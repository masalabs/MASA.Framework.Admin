using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public abstract class BackgroundJob : IBackgroundJob
    {
        public abstract void Execute(object args);
    }
}
