using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileProviderSaveParameters : FileProviderParameters
    {
        public Stream BlobStream { get; }

        public FileProviderSaveParameters(
            string containerName,
            FileContainerConfiguration configuration,
            string blobName,
            Stream blobStream,
            CancellationToken cancellationToken = default)
            : base(
                  containerName,
                  configuration,
                  blobName,
                  cancellationToken)
        {
            BlobStream = blobStream ?? throw new ArgumentNullException(nameof(blobStream));
        }
    }
}
