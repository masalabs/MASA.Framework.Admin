using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring.Minio
{
    public static class MinioFileContainerConfigurationExtensions
    {
        public static void AddMinioFileStoring(this IServiceCollection services)
        {

            services.AddOptions();

            services.AddTransient(
                        typeof(IFileContainer<>),
                        typeof(FileContainer<>)
                    );
            services.AddTransient(
                        typeof(IFileProviderSelector),
                        typeof(DefaultFileProviderSelector)
                    );
            services.AddTransient(
                        typeof(IFileProvider),
                        typeof(MinioFileProvider)
                    );
            services.AddTransient(
                        typeof(IFileContainerFactory),
                        typeof(FileContainerFactory)
                    );
        }

        public static FileContainerConfiguration UseMinio(
        this FileContainerConfiguration containerConfiguration,
        Action<MinioFileProviderConfiguration> minioConfigureAction)
        {
            containerConfiguration.ProviderType = typeof(MinioFileProvider);

            minioConfigureAction(new MinioFileProviderConfiguration(containerConfiguration));

            return containerConfiguration;
        }
    }
}
