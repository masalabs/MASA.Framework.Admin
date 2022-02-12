using Minio;
using Minio.Exceptions;

namespace MASA.Framework.Admin.Infrastructures.FileStoring.Minio
{
    public class MinioFileProvider : IFileProvider
    {
        public async Task<bool> DeleteAsync(FileProviderDeleteParameters parameters)
        {
            var client = GetMinioClient(parameters);

            if (await FileExistsAsync(client, parameters.ContainerName, parameters.FileName))
            {
                await client.RemoveObjectAsync(parameters.ContainerName, parameters.FileName);
                return true;
            }

            return false;
        }

        public async Task<bool> ExistsAsync(FileProviderExistsParameters parameters)
        {
            var client = GetMinioClient(parameters);

            return await FileExistsAsync(client, parameters.ContainerName, parameters.FileName);
        }

        public async Task<Stream> GetOrNullAsync(FileProviderGetParameters parameters)
        {
            var client = GetMinioClient(parameters);

            if (!await FileExistsAsync(client, parameters.ContainerName, parameters.FileName))
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            await client.GetObjectAsync(parameters.ContainerName, parameters.FileName, (stream) =>
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
            var configuration = new MinioFileProviderConfiguration(parameters.Configuration);
            var client = GetMinioClient(parameters);

            if (await FileExistsAsync(client, parameters.ContainerName, parameters.FileName))
            {
                throw new Exception($"Saving File '{parameters.FileName}' does already exists in the container '{parameters.ContainerName}'!");
            }

            if (configuration.CreateBucketIfNotExists)
            {
                await CreateBucketIfNotExists(client, parameters.ContainerName);
            }

            await client.PutObjectAsync(parameters.ContainerName, parameters.FileName, parameters.FileStream, parameters.FileStream.Length);
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

        protected virtual async Task<bool> FileExistsAsync(MinioClient client, string containerName, string flieName)
        {
            // Make sure File Container exists.
            if (await client.BucketExistsAsync(containerName))
            {
                try
                {
                    await client.StatObjectAsync(containerName, flieName);
                }
                catch (Exception e)
                {
                    if (e is ObjectNotFoundException)
                    {
                        return false;
                    }

                    throw;
                }

                return true;
            }
            else
            {
                await client.MakeBucketAsync(containerName);
            }

            return false;
        }
    }
}