using Minio;

namespace MASA.Framework.Admin.Infrastructures.FileStoring.Minio
{
    public class MinioFileProvider : IFileProvider
    {
        public async Task<bool> DeleteAsync(FileProviderDeleteParameters parameters)
        {
            var fileName = parameters.FileName;
            var client = GetMinioClient(parameters);
            var containerName = parameters.ContainerName;

            if (await FileExistsAsync(client, containerName, fileName))
            {
                await client.RemoveObjectAsync(containerName, fileName);
                return true;
            }

            return false;
        }

        public async Task<bool> ExistsAsync(FileProviderExistsParameters parameters)
        {
            var fileName = parameters.FileName;
            var client = GetMinioClient(parameters);
            var containerName = parameters.ContainerName;

            return await FileExistsAsync(client, containerName, fileName);
        }

        public async Task<Stream> GetOrNullAsync(FileProviderGetParameters parameters)
        {
            var fileName = parameters.FileName;
            var client = GetMinioClient(parameters);
            var containerName = parameters.ContainerName;
            if (!await FileExistsAsync(client, containerName, fileName))
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            await client.GetObjectAsync(containerName, fileName, (stream) =>
            {
                if (stream != null)
                {
                    stream.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    memoryStream = null;
                }
            });

            return memoryStream;
        }

        public async Task SaveAsync(FileProviderSaveParameters parameters)
        {
            var fileName = parameters.FileName;
            var configuration = new MinioFileProviderConfiguration(parameters.Configuration);
            var client = GetMinioClient(parameters);
            var containerName = parameters.ContainerName;

            if (await FileExistsAsync(client, containerName, fileName))
            {
                throw new Exception($"Saving BLOB '{parameters.FileName}' does already exists in the container '{containerName}'!");
            }

            if (configuration.CreateBucketIfNotExists)
            {
                await CreateBucketIfNotExists(client, containerName);
            }

            await client.PutObjectAsync(containerName, fileName, parameters.BlobStream, parameters.BlobStream.Length);
        }

        protected virtual MinioClient GetMinioClient(FileProviderParameters parameters)
        {
            var configuration = new MinioFileProviderConfiguration(parameters.Configuration);
            var client = new MinioClient(configuration.EndPoint, configuration.AccessKey, configuration.SecretKey);

            if (configuration.WithSSL)
            {
                client.WithSSL();
            }

            return client;
        }

        protected virtual async Task CreateBucketIfNotExists(MinioClient client, string containerName)
        {
            if (!await client.BucketExistsAsync(containerName))
            {
                await client.MakeBucketAsync(containerName);
            }
        }

        protected virtual async Task<bool> FileExistsAsync(MinioClient client, string containerName, string blobName)
        {
            // Make sure Blob Container exists.
            if (await client.BucketExistsAsync(containerName))
            {
                await client.StatObjectAsync(containerName, blobName);

                return true;
            }

            return false;
        }
    }
}