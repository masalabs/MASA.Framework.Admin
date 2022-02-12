using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;

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
                return dic;
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
            var detail = await _dbContext.Dics.SingleAsync(o => o.Id == id);

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

        public async Task<DicViewModel> GetAsync(Guid id)
        {
            var result = await _dbContext.Dics.SingleAsync(o => o.Id == id);

            return new DicViewModel
            {
                CreateTime = result.CreateTime,
                Description = result.Description,
                Enable = result.Enable,
                Id = result.Id,
                ModuleId = result.ModuleId,
                Name = result.Name,
                Type = result.Type,
            };
        }

        public async Task<PagingResult<DicViewModel>> GetPageAsync(DicPagingOptions options)
        {
            var query = _dbContext.Dics.OrderBy(r => r.CreateTime);

            var totalCount = await query.CountAsync();
            var data = await query.Skip((options.PageIndex - 1) * options.PageSize).Take(options.PageSize).Select(s => new DicViewModel
            {
                CreateTime = s.CreateTime,
                Description = s.Description,
                Enable = s.Enable,
                Id = s.Id,
                ModuleId = s.ModuleId,
                Name = s.Name,
                Type = s.Type,
            }).ToListAsync();

            return new PagingResult<DicViewModel>(options.PageIndex, options.PageSize, totalCount, data);
        }

    }
}
