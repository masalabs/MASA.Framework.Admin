using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileProviderSaveParameters : FileProviderParameters
    {
        public Stream FileStream { get; }

        public FileProviderSaveParameters(
            string containerName,
            FileContainerConfiguration configuration,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken = default)
            : base(
                  containerName,
                  configuration,
                  fileName,
                  cancellationToken)
        {
            FileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream));
        }
    }
}
