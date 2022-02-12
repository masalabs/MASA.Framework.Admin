using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    public class DefaultJobInterpreter : IJobInterpreter
    {
        public async Task ExecuteAsync(JobExecutionContext context)
        {
            await Task.Yield();

            var typeName = GetTypeName(context.JobMethod);

            var methodName = GetMethodName(context.JobMethod);

            var assemblyName = GetAssemblyName(typeName);

            var types = Assembly.Load(assemblyName)?.GetTypes();

            var jobType = types?.FirstOrDefault(t => t.FullName == typeName);

            if(jobType == null)
            {
                throw new ArgumentNullException();
            }

            var job = Activator.CreateInstance(jobType);

            if (job == null)
            {
                throw new ArgumentNullException();
            }

            var jobMethod = jobType.GetMethod(methodName);

            if (jobMethod == null)
            {
                throw new ArgumentNullException();
            }


            if(context.JobArgs != null)
            {
                jobMethod.Invoke(job, new[] { context.JobArgs });
            }

            jobMethod.Invoke(job, null);
        }

        private string GetTypeName(string fullName)
        {
            var start = fullName.LastIndexOf('.');
            return fullName.Substring(0, start);
        }

        private string GetMethodName(string fullName)
        {
            var start = fullName.LastIndexOf('.')+1;
            return fullName.Substring(start);
        }

        private string GetAssemblyName(string typeName)
        {
            var start = typeName.LastIndexOf('.');
            return typeName.Substring(0, start);
        }
    }
}
