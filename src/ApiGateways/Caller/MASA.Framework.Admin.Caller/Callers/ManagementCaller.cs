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
        private DictionaryService _dictionaryService;

        protected override string BaseAddress { get; set; } = "https://localhost:7049";

        public ManagementCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(ManagementCaller);
        }

        public DictionaryService DictionaryService => _dictionaryService ?? new DictionaryService(CallerProvider);
    }
}
