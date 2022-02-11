namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogApprovedRecordRepository
    {
        Task AddBlogApprovedRecord(BlogApprovedRecordModel model);
    }
}
