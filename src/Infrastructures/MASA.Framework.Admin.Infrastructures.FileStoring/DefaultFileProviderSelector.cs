using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public class DefaultFileProviderSelector : IFileProviderSelector
    {
        protected IEnumerable<IFileProvider> FileProviders { get; }

        //protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }

        public DefaultFileProviderSelector(
            //IBlobContainerConfigurationProvider configurationProvider,
            IEnumerable<IFileProvider> fileProviders)
        {
            //ConfigurationProvider = configurationProvider;
            FileProviders = fileProviders;
        }

        public virtual IFileProvider Get(string containerName, FileContainerConfiguration configuration)
        {
            if (containerName == null)
            {
                throw new ArgumentNullException(nameof(containerName));
            }

            if (!FileProviders.Any())
            {
                throw new Exception("No File Storage provider was registered! At least one provider must be registered to be able to use the File Storing System.");
            }

            if (configuration.ProviderType == null)
            {
                throw new Exception("No File Storage provider was used! At least one provider must be configured to be able to use the File Storing System.");
            }

            foreach (var provider in FileProviders)
            {
                if (GetUnProxiedType(provider).IsAssignableFrom(configuration.ProviderType))
                {
                    return provider;
                }
            }

            throw new Exception(
                $"Could not find the File Storage provider with the type ({configuration.ProviderType.AssemblyQualifiedName}) configured for the container {containerName} and no default provider was set."
            );
        }

        public Type GetUnProxiedType(object obj)
        {
            var targetField = obj.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
            .FirstOrDefault(f => f.Name == "__target");

            object? target = targetField == null ? obj : targetField.GetValue(obj);

            if (target != null)
            {
                if (target == obj)
                {
                    return obj.GetType().GetTypeInfo().BaseType;
                }

                return target.GetType();
            }

            return obj.GetType();
        }
    }
}
