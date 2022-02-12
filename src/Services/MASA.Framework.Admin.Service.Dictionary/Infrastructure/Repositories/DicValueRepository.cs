using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

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
                return dicValue;
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

        public async Task<DicValueViewModel> GetAsync(Guid id)
        {
            var result = await _dbContext.DicValues.SingleAsync(o => o.Id == id);

            return new DicValueViewModel
            {
                Id = result.Id,
                CreateTime = result.CreateTime,
                Description = result.Description,
                DicId = result.DicId,
                Enable = result.Enable,
                Lable = result.Lable,
                Sort = result.Sort,
                Value = result.Value,
            };
        }

        public async Task<PagingResult<DicValueViewModel>> GetPageAsync(DicValuePagingOptions options)
        {
            var query = _dbContext.DicValues.OrderBy(r => r.Sort);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize).Select(r => new DicValueViewModel
            {
                Id = r.Id,
                CreateTime = r.CreateTime,
                Description = r.Description,
                DicId = r.DicId,
                Enable = r.Enable,
                Lable = r.Lable,
                Sort = r.Sort,
                Value = r.Value,
            }).ToListAsync();

            return new PagingResult<DicValueViewModel>(options.PageIndex, options.PageSize, totalCount, data);
        }
    }
}
