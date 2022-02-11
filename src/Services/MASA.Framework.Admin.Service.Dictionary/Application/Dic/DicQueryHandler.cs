
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries;

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
        public async Task<Domain.Entities.Dic> GetAsync(DicQuery query)
        {
            return await _dicRepository.GetAsync(query.id);
        }

        [EventHandler]
        public async Task<PagingResult<Domain.Entities.Dic>> GetAsync(DicPageQuery query)
        {
            return await _dicRepository.GetPageAsync(query.Options);
        }
    }
}
