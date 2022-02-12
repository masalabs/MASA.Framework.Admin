namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogApprovedRecordRepository
    {
        Task<BlogInfo> AddBlogApprovedRecord(BlogApprovedRecordModel model);

        Task<bool> ExistBlogApprovedRecord(Guid blogInfoId, Guid userId);
    }
}
