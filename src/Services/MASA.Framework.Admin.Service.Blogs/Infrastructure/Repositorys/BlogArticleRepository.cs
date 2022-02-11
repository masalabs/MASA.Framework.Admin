﻿using System.Linq.Expressions;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogArticleRepository : IBlogArticleRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogArticleRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        }

        /// <summary>
        /// 博客列表
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<PagingResult<BlogInfoListViewModel>> GetListAsync(GetBlogArticleOptions options)
        {
            // var query = from blogInfo in _blogDbContext.BlogInfoes
            //             join blogType in _blogDbContext.BlogTypes on blogInfo.TypeId equals blogType.Id into leftBlogType
            //             from blogType in leftBlogType.DefaultIfEmpty()
            //             select new BlogInfoListViewModel()
            //             {
            //                 id = blogInfo.Id,
            //                 typeId = blogInfo.TypeId,
            //                 title = blogInfo.Title,
            //                 state = blogInfo.State,
            //                 typeName = blogType.TypeName,
            //                 content = blogInfo.Content,
            //                 visits = blogInfo.Visits,
            //                 commentCount = blogInfo.CommentCount,
            //                 approvedCount = blogInfo.ApprovedCount,
            //                 remark = blogInfo.Remark,
            //                 CreationTime = blogInfo.CreationTime,
            //             };
            //
            // var pageResult = await query.OrderBy(x => x.CreationTime).PagingAsync(options.PageIndex, options.PageSize);

            return new PagingResult<BlogInfoListViewModel>();

            // return new PagingResult<BlogInfoListViewModel>()
            // {
            //     Data = pageResult.Data,
            //     Page = pageResult.Page,
            //     Size = pageResult.Size,
            //     TotalCount = pageResult.TotalCount
            // };
        }

        /// <summary>
        /// 博客详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 新增博客
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BlogInfo> CreateAsync(BlogInfo model)
        {
            var result = await _blogDbContext.BlogInfoes.AddAsync(model);

            await _blogDbContext.SaveChangesAsync();

            return result.Entity;
        }

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateAsync(BlogInfo model)
        {
            var blogInfo = await _blogDbContext.BlogInfoes.FindAsync(model.Id);

            if (blogInfo != null)
            {
                // TODO: mapping
                
                blogInfo.Content = model.Content;
                blogInfo.Remark = model.Remark;
                blogInfo.State = model.State;
                blogInfo.Title = model.Title;
                blogInfo.Visits = model.Visits;
                blogInfo.ApprovedCount = model.ApprovedCount;
                blogInfo.CommentCount = model.CommentCount;
                blogInfo.IsShow = model.IsShow;
                blogInfo.TypeId = model.TypeId;
                
                _blogDbContext.Update(blogInfo);
                await _blogDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogInfos = await _blogDbContext.BlogInfoes.Where(type => ids.Contains(type.Id)).ToListAsync();

            foreach (var blogInfo in blogInfos)
            {
                blogInfo.IsDeleted = true;
                blogInfo.DeletionTime = DateTime.UtcNow;
            }

            _blogDbContext.UpdateRange(blogInfos);
            await _blogDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 获取用户个人列表
        /// </summary>
        /// <param name="opions"></param>
        /// <returns></returns>
        public async Task<PagingResult<BlogInfoListViewModel>> GetBlogArticleByUser(GetBlogArticleUserOptions options)
        {
            var query = from blogInfo in _blogDbContext.BlogInfoes
                        join blogType in _blogDbContext.BlogTypes on blogInfo.TypeId equals blogType.Id into leftBlogType
                        from blogType in leftBlogType.DefaultIfEmpty()
                        where blogInfo.CreatorUserId == options.Author
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
                        };

            var pageResult = await query.OrderByDescending(x => x.CreationTime).PagingAsync(options.PageIndex, options.PageSize);

            return new PagingResult<BlogInfoListViewModel>()
            {
                Data = pageResult.Data,
                Page = pageResult.Page,
                Size = pageResult.Size,
                TotalCount = pageResult.TotalCount
            };
        }

        /// <summary>
        /// 追加阅读数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddVisits(AddBlogVisitModel model)
        {
            var blogInfo = await _blogDbContext.BlogInfoes.FindAsync(model.BlogId);

            if (blogInfo != null)
            {
                blogInfo.Visits++;
                _blogDbContext.Update(blogInfo);
                await _blogDbContext.SaveChangesAsync();
            }
        }
    }
}
