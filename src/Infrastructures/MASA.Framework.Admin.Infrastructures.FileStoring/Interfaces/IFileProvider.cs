namespace MASA.Framework.Admin.Infrastructures.FileStoring
{
    public interface IFileProvider
    {
        Task SaveAsync(FileProviderSaveParameters parameters);

        Task<bool> DeleteAsync(FileProviderDeleteParameters parameters);

        Task<bool> ExistsAsync(FileProviderExistsParameters parameters);

        Task<Stream> GetOrNullAsync(FileProviderGetParameters parameters);
    }
}