using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Framework.Admin.Service.Blogs.Model.BlogAdvertisingPictures.Options;
using MASA.Framework.Admin.Service.Blogs.Model.BlogAdvertisingPictures.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            var model = await _blogDbContext.BlogAdvertisingPictures.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();

            return model.Entity;
        }

        public async Task UpdateAsync(BlogAdvertisingPictures entity)
        {
            var blogTypes = await _blogDbContext.BlogAdvertisingPictures.FindAsync(entity.Id);

            if (blogTypes != null)
            {
                _blogDbContext.Update(entity);
                await _blogDbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogTypes = await _blogDbContext.BlogAdvertisingPictures.Where(type => ids.Contains(type.Id)).ToListAsync();

            foreach (var blogType in blogTypes)
            {
                blogType.IsDeleted = true;
            }

            _blogDbContext.UpdateRange(blogTypes);
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task<PageResult<BlogAdvertisingPicturesListViewModel>> GetListAsync(GetBlogAdvertisingPicturesOption options)
        {
            var paging = await _blogDbContext.BlogAdvertisingPictures.OrderByDescending(type => type.CreationTime).Select(pictures => new BlogAdvertisingPicturesListViewModel
            {
                Id = pictures.Id,
                Title = pictures.Title,
                Pic = pictures.Pic,
                Sort = pictures.Sort,
                Type = pictures.Type,
                Location = pictures.Location,
                CreationTime = pictures.CreationTime,
                LastModificationTime = pictures.LastModificationTime

            }).PagingAsync(options.PageIndex, options.PageSize);


            return paging;
        }
    }
}
