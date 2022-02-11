
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic
{
    public class DicQueryHandler
    {
        private readonly IDicRepository _dicRepository;

        public DicQueryHandler(IDicRepository dicRepository)
        {
            _dicRepository = dicRepository;
        }

        [EventHandler]
        public async Task<Domain.Entities.Dic> GetAsync(Guid id)
        {
            return await _dicRepository.GetAsync(id);
        }

        [EventHandler]
        public async Task<PagingResult<Domain.Entities.Dic>> GetAsync(DicPagingOptions dicPageOptions)
        {
            return await _dicRepository.GetPageAsync(dicPageOptions);
        }
    }
}
