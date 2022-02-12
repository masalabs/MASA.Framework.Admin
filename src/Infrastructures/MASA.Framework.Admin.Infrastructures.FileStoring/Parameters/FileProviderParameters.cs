using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public abstract class FileProviderParameters
    {
        public string ContainerName { get; }

        public FileContainerConfiguration Configuration { get; }

        public string FileName { get; }

        public CancellationToken CancellationToken { get; }

        protected FileProviderParameters(
            string containerName,
            FileContainerConfiguration configuration,
            string fileName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException($"{nameof(containerName)} can not be null, empty or white space!", containerName);
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} can not be null, empty or white space!", fileName);
            }

            ContainerName = containerName;
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            FileName = fileName;
            CancellationToken = cancellationToken;
        }
    }
}
