using System.Linq.Expressions;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogAdvertisingPicturesRepository : IBlogAdvertisingPicturesRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogAdvertisingPicturesRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<BlogAdvertisingPictures> CreateAsync(BlogAdvertisingPictures entity)
        {
            entity.Status = false;

            var model = await _blogDbContext.BlogAdvertisingPictures.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();

            return model.Entity;
        }

        public async Task UpdateAsync(BlogAdvertisingPictures entity)
        {
            _blogDbContext.Update(entity);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogTypes = await _blogDbContext.BlogAdvertisingPictures.Where(type => ids.Contains(type.Id))
                .ToListAsync();

            //foreach (var blogType in blogTypes)
            //{
            //    blogType.IsDeleted = true;
            //}

            _blogDbContext.RemoveRange(blogTypes);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task<PagingResult<BlogAdvertisingPicturesListViewModel>> GetListAsync(
            GetBlogAdvertisingPicturesOption options)
        {
            Expression<Func<BlogAdvertisingPictures, bool>> where = pictures => true;
            where = where.And(options.Type.HasValue, pictures => pictures.Type == options.Type);

            var paging = await _blogDbContext.BlogAdvertisingPictures.Where(where)
                .OrderByDescending(type => type.CreationTime)
                .Select(pictures => new BlogAdvertisingPicturesListViewModel
                {
                    Id = pictures.Id,
                    Title = pictures.Title,
                    Pic = pictures.Pic,
                    Sort = pictures.Sort,
                    Type = pictures.Type,
                    Status = pictures.Status,
                    CreationTime = pictures.CreationTime,
                    LastModificationTime = pictures.LastModificationTime
                }).PagingAsync(options.PageIndex, options.PageSize);

            return paging;
        }

        public async Task UpdateByStatusAsync(Guid id, bool status)
        {
            var blogAdvertisingPictures = await _blogDbContext.BlogAdvertisingPictures.FindAsync(id);

            if (blogAdvertisingPictures != null)
            {
                _blogDbContext.Update(blogAdvertisingPictures);
                await _blogDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 前端广告位取得
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<List<BlogAdvertisingPicturesListViewModel>> GetBlogFrontListAsync(
            GetBlogAdvertisingPicturesFrontOption options)
        {
            return await _blogDbContext.BlogAdvertisingPictures
                .Where(x => (options.Types == null || options.Types.Contains(x.Type)) && x.Status && !x.IsDeleted)
                .OrderBy(type => type.Sort)
                .Select(pictures => new BlogAdvertisingPicturesListViewModel
                {
                    Id = pictures.Id,
                    Title = pictures.Title,
                    Pic = pictures.Pic,
                    Sort = pictures.Sort,
                    Type = pictures.Type,
                    Status = pictures.Status,

                    CreationTime = pictures.CreationTime,
                    LastModificationTime = pictures.LastModificationTime
                }).ToListAsync();
        }
    }
}