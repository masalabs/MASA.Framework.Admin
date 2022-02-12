using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class FileContainerFactory : IFileContainerFactory
    {
        protected IFileProviderSelector ProviderSelector { get; }

        protected IServiceProvider ServiceProvider { get; }

        protected FileStoringOptions FileStoringOptions { get; }

        public FileContainerFactory(
        IFileProviderSelector providerSelector,
        IServiceProvider serviceProvider,
        IOptions<FileStoringOptions> fileStoringOptions)
        {
            ProviderSelector = providerSelector;
            ServiceProvider = serviceProvider;
            FileStoringOptions = fileStoringOptions.Value;
        }

        public virtual IFileContainer Create(string name)
        {
            var configuration = FileStoringOptions.Container;

            return new FileContainer(
                name,
                configuration,
                ProviderSelector.Get(name, configuration),
                ServiceProvider
                );
        }
    }
}
