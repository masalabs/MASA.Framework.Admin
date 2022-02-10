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
