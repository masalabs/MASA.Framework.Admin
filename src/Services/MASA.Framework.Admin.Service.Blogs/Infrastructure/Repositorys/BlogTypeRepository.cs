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
            await ExistTypeName(entity);

            var model = await _blogDbContext.BlogTypes.AddAsync(entity);

            await _blogDbContext.SaveChangesAsync();
            _blogDbContext.Database.CurrentTransaction?.Commit();

            return model.Entity;
        }

        private async Task ExistTypeName(BlogType entity)
        {
            if (await _blogDbContext.BlogTypes.AnyAsync(
                x => x.TypeName == entity.TypeName && !x.Id.Equals(entity.Id)))
                throw new Exception($"{entity.TypeName}已存在！");
        }

        public async Task UpdateAsync(BlogType entity)
        {
            var blogTypes = await _blogDbContext.BlogTypes.FindAsync(entity.Id);

            if (blogTypes != null)
            {
                await ExistTypeName(entity);

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
                                            Id = blogInfo.Id,
                                            TypeId = blogInfo.TypeId,
                                            Title = blogInfo.Title,
                                            TypeName = blogType.TypeName
                                        }).ToListAsync();

                if (blogInfoes.Any())
                {
                    throw new UserFriendlyException($"{string.Join(",", blogInfoes.Select(x => x.TypeName).ToList())}已被使用,删除失败！");
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
            var query = _blogDbContext.BlogTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.TypeName))
            {
                query = query.Where(x => x.TypeName.Contains(options.TypeName));
            }

            return query.OrderByDescending(type => type.CreationTime).Select(type =>
                new BlogTypePagingViewModel
                {
                    Id = type.Id,
                    CreationTime = type.CreationTime,
                    TypeName = type.TypeName
                }).PagingAsync(options.PageIndex, options.PageSize);
        }

        public Task<List<BlogTypeCondensedViewModel>> GetCondensedListAsync()
        {
            return _blogDbContext.BlogTypes.OrderBy(b => b.TypeName)
                .Select(b => new BlogTypeCondensedViewModel(b.Id, b.TypeName)).ToListAsync();
        }
    }
}