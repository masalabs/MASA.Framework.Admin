namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogArticleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<PagingResult<BlogInfoListViewModel>> GetListAsync(GetBlogArticleOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BlogInfoListViewModel> GetAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        Task<BlogInfo> CreateAsync(BlogInfo entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(BlogInfo entity);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task RemoveAsync(params Guid[] ids);

        /// <summary>
        /// 获取用户个人列表
        /// </summary>
        /// <param name="opions"></param>
        /// <returns></returns>
        Task<PagingResult<BlogInfoListViewModel>> GetBlogArticleByUser(GetBlogArticleUserOptions options);

        /// <summary>
        /// 追加阅读量
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddVisits(AddBlogVisitModel model);
    }
}