using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.BackgroundJobs
{
    [Serializable]
    public class BackgroundJobExecutionException : Exception
    {
        public string JobType { get; set; }

        public object JobArgs { get; set; }

        public BackgroundJobExecutionException(string message, Exception innerException)
            : base(message, innerException) { }

        public BackgroundJobExecutionException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }
    }
}
