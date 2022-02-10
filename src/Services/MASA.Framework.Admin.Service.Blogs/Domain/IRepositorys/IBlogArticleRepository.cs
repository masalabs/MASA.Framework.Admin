using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogArticleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<PageResult<BlogInfoListViewModel>> GetListAsync(GetBlogArticleOptions options);

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
    }
}
