using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using System.Linq.Expressions;

namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public class DicRepository : IDicRepository
    {
        private readonly DictionaryDbContext _dbContext;

        public DicRepository(DictionaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Dic> AddAsync(Dic dic)
        {
            _dbContext.Add(dic);

            try
            {
                await _dbContext.SaveChangesAsync();
                return dic;
            }
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2627)
            {
                return await GetAsync(dic.Id);
            }
        }

        public async Task DeleteAllAsync(List<Guid> guids)
        {
            var list = await _dbContext.Dics.Where(q => guids.Contains(q.Id)).ToListAsync();

            if (list?.Count > 0)
            {
                _dbContext.RemoveRange(list);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var detail = await GetAsync(id);

            if (detail != null)
            {
                _dbContext.Remove(detail);

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Dic> UpdateAsync(Dic dic)
        {
            _dbContext.Update(dic);
            await _dbContext.SaveChangesAsync();
            return dic;
        }

        public async Task<Dic> GetAsync(Guid id)
        {
            return await _dbContext.Dics.SingleAsync(o => o.Id == id);
        }

        public async Task<PagingResult<Dic>> GetPageAsync(DicPagingOptions options)
        {
            var query = _dbContext.Dics.OrderBy(r => r.CreateTime);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize).ToListAsync();

            return new PagingResult<Dic>(options.PageIndex, options.PageSize, totalCount, data);
        }

    }
}
