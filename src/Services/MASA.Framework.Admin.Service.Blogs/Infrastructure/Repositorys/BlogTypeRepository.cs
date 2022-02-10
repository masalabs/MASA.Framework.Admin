using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogTypeRepository : IBlogTypeRepository
    {
        private readonly BlogDbContext _blogDbContext;
        public BlogTypeRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<BlogType> CreateAsync(BlogType entity)
        {
            var model = await _blogDbContext.BlogTypes.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();

            return model.Entity;
        }

        public async Task UpdateAsync(BlogType entity)
        {
            var blogTypes = await _blogDbContext.BlogTypes.FindAsync(entity.Id);

            if (blogTypes != null)
            {
                blogTypes.TypeName = entity.TypeName;
                _blogDbContext.Update(blogTypes);
                await _blogDbContext.SaveChangesAsync();
            }

        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogTypes = await _blogDbContext.BlogTypes.Where(type => ids.Contains(type.Id)).ToListAsync();

            foreach (var blogType in blogTypes)
            {
                blogType.IsDeleted = true;
            }

            _blogDbContext.UpdateRange(blogTypes);
            await _blogDbContext.SaveChangesAsync();
        }

        //public async Task<List<>>
    }
}
