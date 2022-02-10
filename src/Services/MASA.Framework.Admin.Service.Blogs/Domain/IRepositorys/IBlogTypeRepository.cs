using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys
{
    public interface IBlogTypeRepository
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        Task<BlogType> CreateAsync(BlogType entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(BlogType entity);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task RemoveAsync(params Guid[] ids);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<PageResult<BlogTypePagingViewModel>> GetListAsync(GetBlogTypePagingOption options);

    }
}
