using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;
using MASA.Framework.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogArticleRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        public async Task<PageResult<BlogInfoListViewModel>> GetListAsync(GetBlogArticleOptions options)
        {
            var query = from blogInfo in _blogDbContext.BlogInfoes
                        join blogType in _blogDbContext.BlogTypes on blogInfo.TypeId equals blogType.Id into leftBlogType
                        from blogType in leftBlogType.DefaultIfEmpty()
                        select new BlogInfoListViewModel()
                        {
                            id = blogInfo.Id,
                            typeId=blogInfo.TypeId,
                            title = blogInfo.Title,
                            state = blogInfo.State,
                            typeName = blogType.TypeName,
                            content = blogInfo.Content,
                            visits = blogInfo.Visits,
                            commentCount = blogInfo.CommentCount,
                            approvedCount = blogInfo.ApprovedCount,
                            remark = blogInfo.Remark,
                            CreationTime = blogInfo.CreationTime
                        };

            var pageResult = await query.OrderBy(x => x.CreationTime).PagingAsync(options.PageIndex, options.PageSize);

            return new PageResult<BlogInfoListViewModel>()
            {
                Data = pageResult.Data,
                Page = pageResult.Page,
                Size = pageResult.Size,
                TotalCount = pageResult.TotalCount
            };
        }

        public async Task<BlogInfoListViewModel> GetAsync(Guid id)
        {
            var data = await (from blogInfo in _blogDbContext.BlogInfoes
                               join blogType in _blogDbContext.BlogTypes on blogInfo.TypeId equals blogType.Id
                               into leftBlogType
                               from blogType in leftBlogType.DefaultIfEmpty()
                               where blogInfo.Id == id
                               select new BlogInfoListViewModel()
                               {
                                   id = blogInfo.Id,
                                   typeId = blogInfo.TypeId,
                                   title = blogInfo.Title,
                                   state = blogInfo.State,
                                   typeName = blogType.TypeName,
                                   content = blogInfo.Content,
                                   visits = blogInfo.Visits,
                                   commentCount = blogInfo.CommentCount,
                                   approvedCount = blogInfo.ApprovedCount,
                                   remark = blogInfo.Remark,
                                   CreationTime = blogInfo.CreationTime
                               }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<BlogInfo> CreateAsync(BlogInfo model)
        {
            var result = await _blogDbContext.BlogInfoes.AddAsync(model);

            await _blogDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task UpdateAsync(BlogInfo model)
        {
            var blogInfo = await _blogDbContext.BlogInfoes.FindAsync(model.Id);

            if (blogInfo != null)
            {
                var updateBlogInfo = new Mapping<BlogInfo, BlogInfo>().Map(model, blogInfo);

                _blogDbContext.Update(updateBlogInfo);
                await _blogDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogInfos = await _blogDbContext.BlogInfoes.Where(type => ids.Contains(type.Id)).ToListAsync();

            foreach (var blogInfo in blogInfos)
            {
                blogInfo.IsDeleted = true;
            }

            _blogDbContext.UpdateRange(blogInfos);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}
