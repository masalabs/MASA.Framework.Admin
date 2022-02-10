using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogArticleRepository
    {
        Task<PageResult<BlogInfoListViewModel>> GetListAsync(GetBlogArticleOptions options);

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
    }
}
