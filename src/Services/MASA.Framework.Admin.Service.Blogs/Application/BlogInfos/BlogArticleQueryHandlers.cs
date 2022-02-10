using MASA.Contrib.Dispatcher.Events;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Querys;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;
using MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.ViewModel;
using MASA.Framework.Data.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleQueryHandlers
    {
        private readonly IBlogArticleRepository _blogArticleRepository;

        public BlogArticleQueryHandlers(IBlogArticleRepository blogArticleRepository)
        {
            _blogArticleRepository = blogArticleRepository;
        }

        [EventHandler]
        public async Task BlogArticleQueryAsync(BlogArticleQuery query)
        {
            var blogArticle = await _blogArticleRepository.GetListAsync(query.Options);

            query.Result = blogArticle;
        }

        [EventHandler]
        public async Task BlogArticleDetailsQueryAsync(BlogArticleDetailsQuery query)
        {
            var blogArticle = await _blogArticleRepository.GetAsync(query.Id);

            query.Result = blogArticle;
        }
    }
}
