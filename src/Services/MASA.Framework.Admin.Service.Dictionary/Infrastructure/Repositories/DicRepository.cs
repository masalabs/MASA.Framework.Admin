using Microsoft.Data.SqlClient;

namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public class DicRepository : IDicRepository
    {
        private readonly DicDbContext _dicDbContext;

        public DicRepository(DicDbContext dicDbContext)
        {
            _dicDbContext = dicDbContext;
        }

        public async Task<Dic> AddAsync(Dic dic)
        {
            _dicDbContext.Add(dic);

            try
            {
                await _dicDbContext.SaveChangesAsync();
                return dic;
            }
            catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number == 2627)
            {
                return await GetAsync(dic.Id);
            }
        }

        public async Task DeleteAllAsync(List<Guid> guids)
        {
            var list = await _dicDbContext.Dics.Where(q => guids.Contains(q.Id)).ToListAsync();

            if (list.Count > 0)
            {
                _dicDbContext.RemoveRange(list);

                await _dicDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var detail = await GetAsync(id);

            if (detail != null)
            {
                _dicDbContext.Remove(detail);

                await _dicDbContext.SaveChangesAsync();
            }
        }

        public async Task<Dic> GetAsync(Guid id)
        {
            return await _dicDbContext.Dics.SingleAsync(o => o.Id == id);
        }

        public async Task<Dic> UpdateAsync(Dic dic)
        {
            _dicDbContext.Update(dic);
            await _dicDbContext.SaveChangesAsync();
            return dic;
        }
    }
}
