using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public class HttpJobInterpreter : IJobInterpreter
    {

        private readonly HttpClient _httpClient;

        public HttpJobInterpreter(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task ExecuteAsync(JobExecutionContext context)
        {
            var content = new StringContent(context.JobArgs.ToString(), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(context.JobUri, content);
        }
    }
}
