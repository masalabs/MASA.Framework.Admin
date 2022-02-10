using MASA.Contrib.Dispatcher.Events;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Querys;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Framework.Admin.Service.Blogs.Model.BlogType.Options;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes
{
    public class BlogTypeQueryHandler
    {
        private readonly IBlogTypeRepository _blogTypeRepository;

        public BlogTypeQueryHandler(IBlogTypeRepository blogTypeRepository)
        {
            this._blogTypeRepository = blogTypeRepository;
        }

        [EventHandler]
        public async Task GetListAsync(GetBlogTypePagingQuery query)
        {
            var list = await this._blogTypeRepository.GetListAsync(query.Request);

            query.Result = list;
        }
    }
}
