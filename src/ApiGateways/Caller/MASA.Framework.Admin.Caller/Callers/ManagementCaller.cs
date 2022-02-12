using MASA.Framework.Admin.Caller.Management;
using MASA.Utils.Caller.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Caller.Callers
{
    public class ManagementCaller : HttpClientCallerBase
    {
        private DicService _dicService;

        private DicValueService _dicValueService;

        private JobService _jobService;

        protected override string BaseAddress { get; set; } = "https://localhost:6383";

        public ManagementCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(ManagementCaller);
        }

        public DicService DicService => _dicService ?? new DicService(CallerProvider);

        public DicValueService DicValueService => _dicValueService ?? new DicValueService(CallerProvider);

        public JobService JobService => _jobService ?? new JobService(CallerProvider);
    }
}
