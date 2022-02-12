using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;

namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public class DicValueRepository : IDicValueRepository
    {
        private readonly DictionaryDbContext _dbContext;

        public DicValueRepository(DictionaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DicValue> AddAsync(DicValue dicValue)
        {
            _dbContext.Add(dicValue);

            try
            {
                await _dbContext.SaveChangesAsync();
                return dicValue;
            }
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2627)
            {
                return await GetAsync(dicValue.Id);
            }
        }

        public async Task DeleteAllAsync(List<Guid> guids)
        {
            var list = await _dbContext.DicValues.Where(q => guids.Contains(q.Id)).ToListAsync();

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

        public async Task<DicValue> UpdateAsync(DicValue dicValue)
        {
            _dbContext.Update(dicValue);
            await _dbContext.SaveChangesAsync();
            return dicValue;
        }

        public async Task<DicValue> GetAsync(Guid id)
        {
            return await _dbContext.DicValues.SingleAsync(o => o.Id == id);
        }

        public async Task<PagingResult<DicValue>> GetPageAsync(DicValuePagingOptions options)
        {
            var query = _dbContext.DicValues.OrderBy(r => r.Sort);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize).ToListAsync();

            return new PagingResult<DicValue>(options.PageIndex, options.PageSize, totalCount, data);
        }
    }
}
