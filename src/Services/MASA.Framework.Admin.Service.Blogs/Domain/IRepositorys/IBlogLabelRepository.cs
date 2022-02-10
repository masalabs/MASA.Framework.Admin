namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogLabelRepository
    {
        Task<List<Guid>> CreateBatchAsync(List<string> labels);
    }
}
