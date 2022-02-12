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
        public async Task GetAsync(DicQuery query)
        {
            query.Result = await _dicRepository.GetAsync(query.id);
        }

        [EventHandler]
        public async Task GetAsync(DicPageQuery query)
        {
            query.Result = await _dicRepository.GetPageAsync(query.Options);
        }
    }
}
