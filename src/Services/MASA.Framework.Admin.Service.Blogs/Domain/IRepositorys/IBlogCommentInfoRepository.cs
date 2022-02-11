namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogCommentInfoRepository
    {
        Task<BlogCommentInfo> CreateAsync(BlogCommentInfo entity);

        Task<Guid> RemoveAsync(Guid id);

        Task<PageResult<BlogCommentInfoListViewModel>> GetBlogComments(
            GetBlogCommentInfoOptions options);
    }
}
