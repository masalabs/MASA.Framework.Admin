namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogCommentInfoRepository
    {
        Task<BlogCommentInfo> CreateAsync(BlogCommentInfo entity);

        Task<Guid> RemoveAsync(Guid id);

        Task<PagingResult<BlogCommentInfoListViewModel>> GetBlogComments(
            GetBlogCommentInfoOptions options);
    }
}
