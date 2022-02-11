using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;

namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public interface IDicRepository
    {
        Task<Dic> AddAsync(Dic dic);

        Task<Dic> UpdateAsync(Dic dic);

        Task DeleteAsync(Guid id);

        Task DeleteAllAsync(List<Guid> guids);

        Task<Dic> GetAsync(Guid id);

        Task<PagingResult<Dic>> GetPageAsync(DicPagingOptions dicPageOptions);
    }
}
