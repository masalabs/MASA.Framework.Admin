using MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues
{
    public record DicValueQueryHandler
    {
        private readonly IDicValueRepository _dicValueRepository;

        public DicValueQueryHandler(IDicValueRepository  dicValueRepository)
        {
            _dicValueRepository = dicValueRepository;
        }

        [EventHandler]
        public async Task GetAsync(DicValueQuery query)
        {
            query.Result = await _dicValueRepository.GetAsync(query.id);
        }

        [EventHandler]
        public async Task GetAsync(DicValuePageQuery query)
        {
            query.Result = await _dicValueRepository.GetPageAsync(query.Options);
        }
    }
}
