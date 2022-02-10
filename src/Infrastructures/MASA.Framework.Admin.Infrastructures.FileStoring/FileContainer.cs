using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileContainer<TContainer> : IFileContainer<TContainer>
    where TContainer : class
    {
        private readonly IFileContainer _container;

        public FileContainer(IFileContainerFactory fileContainerFactory)
        {
            _container = fileContainerFactory.Create<TContainer>();
        }

        public Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            return _container.SaveAsync(
                name,
                stream,
                overrideExisting,
                cancellationToken
            );
        }

        public Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.DeleteAsync(
                name,
                cancellationToken
            );
        }

        public Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.ExistsAsync(
                name,
                cancellationToken
            );
        }

        public Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetAsync(
                name,
                cancellationToken
            );
        }

        public Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetOrNullAsync(
                name,
                cancellationToken
            );
        }
    }

    public class FileContainer : IFileContainer
    {
        protected string ContainerName { get; }

        protected FileContainerConfiguration Configuration { get; }

        protected IFileProvider Provider { get; }

        protected IServiceProvider ServiceProvider { get; }

        public FileContainer(
            string containerName,
            FileContainerConfiguration configuration,
            IFileProvider provider,
            IServiceProvider serviceProvider)
        {
            ContainerName = containerName;
            Configuration = configuration;
            Provider = provider;
            ServiceProvider = serviceProvider;
        }

        public virtual async Task SaveAsync(
            string fileName,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            await Provider.SaveAsync(
                new FileProviderSaveParameters(
                    ContainerName,
                    Configuration,
                    fileName,
                    stream,
                    cancellationToken
                )
            );
        }

        public virtual async Task<bool> DeleteAsync(
            string fileName,
            CancellationToken cancellationToken = default)
        {
            return await Provider.DeleteAsync(
                new FileProviderDeleteParameters(
                    ContainerName,
                    Configuration,
                    fileName,
                    cancellationToken
                )
            );
        }

        public virtual async Task<bool> ExistsAsync(
            string fileName,
            CancellationToken cancellationToken = default)
        {
            return await Provider.ExistsAsync(
                new FileProviderExistsParameters(
                    ContainerName,
                    Configuration,
                    fileName,
                    cancellationToken
                )
            );
        }

        public virtual async Task<Stream> GetAsync(
            string fileName,
            CancellationToken cancellationToken = default)
        {
            var stream = await GetOrNullAsync(fileName, cancellationToken);

            if (stream == null)
            {
                //TODO: Consider to throw some type of "not found" exception and handle on the HTTP status side
                throw new Exception(
                    $"Could not found the requested File '{fileName}' in the container '{ContainerName}'!");
            }

            return stream;
        }

        public virtual async Task<Stream> GetOrNullAsync(
            string fileName,
            CancellationToken cancellationToken = default)
        {
            return await Provider.GetOrNullAsync(
                new FileProviderGetParameters(
                    ContainerName,
                    Configuration,
                    fileName,
                    cancellationToken
                )
            );
        }
    }
}
