namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Repositories
{
    public interface IDicValueRepository
    {
        Task<DicValue> AddAsync(DicValue dicValue);

        Task<DicValue> UpdateAsync(DicValue dicValue);

        Task DeleteAsync(Guid id);

        Task DeleteAllAsync(List<Guid> guids);
    }
}
