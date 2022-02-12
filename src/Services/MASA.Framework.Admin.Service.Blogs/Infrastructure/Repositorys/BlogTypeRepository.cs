namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Repositorys
{
    public class BlogTypeRepository : IBlogTypeRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogTypeRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<Domain.Entities.BlogType> CreateAsync(Domain.Entities.BlogType entity)
        {
            var blogType = await _blogDbContext.BlogTypes.FirstOrDefaultAsync(x => x.TypeName == entity.TypeName);

            if (blogType != null)
            {
                throw new Exception($"{entity.TypeName}已存在！");
            }

            var model = await _blogDbContext.BlogTypes.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();
            _blogDbContext.Database.CurrentTransaction?.Commit();

            return model.Entity;
        }

        public async Task UpdateAsync(Domain.Entities.BlogType entity)
        {
            var blogTypes = await _blogDbContext.BlogTypes.FindAsync(entity.Id);

            if (blogTypes != null)
            {
                blogTypes.TypeName = entity.TypeName;
                _blogDbContext.Update(blogTypes);
                await _blogDbContext.SaveChangesAsync();
                _blogDbContext.Database.CurrentTransaction?.Commit();
            }
        }

        public async Task RemoveAsync(params Guid[] ids)
        {
            var blogTypes = await _blogDbContext.BlogTypes.Where(type => ids.Contains(type.Id)).ToListAsync();

            if (blogTypes.Any())
            {
                var blogTypeIds = blogTypes.Select(x => x.Id).ToList();

                var blogInfoes = await (from blogInfo in _blogDbContext.BlogInfoes
                                        join blogType in _blogDbContext.BlogTypes on blogInfo.TypeId equals blogType.Id
                                        where blogTypeIds.Contains(blogInfo.TypeId)
                                        select new BlogInfoListViewModel()
                                        {
                                            id = blogInfo.Id,
                                            typeId = blogInfo.TypeId,
                                            title = blogInfo.Title,
                                            typeName = blogType.TypeName
                                        }).ToListAsync();

                if (blogInfoes.Any())
                {
                    throw new Exception($"{string.Join(",", blogInfoes.Select(x => x.typeName).ToList())}已被使用,删除失败！");
                }
            }

            foreach (var blogType in blogTypes)
            {
                blogType.IsDeleted = true;
            }

            _blogDbContext.UpdateRange(blogTypes);
            await _blogDbContext.SaveChangesAsync();
            _blogDbContext.Database.CurrentTransaction?.Commit();
        }

        public Task<PagingResult<BlogTypePagingViewModel>> GetListAsync(GetBlogTypePagingOption options)
        {
            return _blogDbContext.BlogTypes.OrderByDescending(type => type.CreationTime).Select(type =>
                new BlogTypePagingViewModel
                {
                    Id = type.Id,
                    CreationTime = type.CreationTime,
                    TypeName = type.TypeName,
                    LastModificationTime = type.LastModificationTime
                }).PagingAsync(options.PageIndex, options.PageSize);
        }

        public Task<List<BlogTypeCondensedViewModel>> GetCondensedListAsync()
        {
            return _blogDbContext.BlogTypes.OrderBy(b => b.TypeName)
                .Select(b => new BlogTypeCondensedViewModel
                {
                    Id = b.Id,
                    Name = b.TypeName
                }).ToListAsync();
        }
    }
}