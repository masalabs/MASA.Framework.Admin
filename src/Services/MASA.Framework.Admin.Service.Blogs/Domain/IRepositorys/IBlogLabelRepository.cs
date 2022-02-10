namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogLabelRepository
    {
        Task<List<Guid>> CreateBatchAsync(IEnumerable<string> labels);

        Task CreateBlogLabelRelationBatchAsync(IEnumerable<BlogLabelRelationship> relationships);

        Task DeleteBlogLabelRelationBatchAsync(IEnumerable<Guid> ids);

        Task<List<BlogLabelRelationsViewModel>> GetRelationsByBlog(Guid blogId);
    }
}
