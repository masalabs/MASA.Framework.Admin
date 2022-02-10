using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Infrastructures.FileStoring.Minio
{
    public static class MinioFileProviderConfigurationNames
    {
        public const string BucketName = "Minio.BucketName";
        public const string EndPoint = "Minio.EndPoint";
        public const string AccessKey = "Minio.AccessKey";
        public const string SecretKey = "Minio.SecretKey";
        public const string WithSSL = "Minio.WithSSL";
        public const string CreateBucketIfNotExists = "Minio.CreateBucketIfNotExists";
    }
}
