using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public interface IDicValueRepository
    {
        Task<DicValue> AddAsync(DicValue dicValue);

        Task<DicValue> UpdateAsync(DicValue dicValue);

        Task DeleteAsync(Guid id);

        Task DeleteAllAsync(List<Guid> guids);

        Task<DicValueViewModel> GetAsync(Guid id);

        Task<PagingResult<DicValueViewModel>> GetPageAsync(DicValuePagingOptions options);
    }
}
