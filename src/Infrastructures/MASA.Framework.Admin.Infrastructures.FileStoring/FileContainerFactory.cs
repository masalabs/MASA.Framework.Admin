using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructure.FileStoring
{
    public class FileContainerFactory : IFileContainerFactory
    {
        protected IFileProviderSelector ProviderSelector { get; }

        protected IServiceProvider ServiceProvider { get; }

        public FileContainerFactory(
        IFileProviderSelector providerSelector,
        IServiceProvider serviceProvider)
        {
            ProviderSelector = providerSelector;
            ServiceProvider = serviceProvider;
        }

        public virtual IFileContainer Create(string name, FileContainerConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
