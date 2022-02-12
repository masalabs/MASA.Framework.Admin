using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileProviderExistsParameters : FileProviderParameters
    {
        public FileProviderExistsParameters(
            string containerName, 
            FileContainerConfiguration configuration, 
            string fileName, 
            CancellationToken cancellationToken = default) 
            : base(
                  containerName, 
                  configuration,
                  fileName, 
                  cancellationToken)
        {
        }
    }
}
