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
            //_container = fileContainerFactory.Create<TContainer>();
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
}
