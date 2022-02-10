using MASA.Contrib.Dispatcher.Events;
using MASA.Framework.Admin.Service.Blogs.Application.BlogInfos.Commands;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogInfos
{
    public class BlogArticleCommandHandlers
    {
        private readonly IBlogArticleRepository _blogArticleRepository;

        public BlogArticleCommandHandlers(IBlogArticleRepository blogArticleRepository)
        {
            this._blogArticleRepository = blogArticleRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogInfoCommand command)
        {
            await _blogArticleRepository.CreateAsync(new()
            {
                Title = command.Request.Title,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                State = command.Request.State,
            });
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogInfoCommand command)
        {
            await _blogArticleRepository.UpdateAsync(new()
            {
                Id = command.Request.Id,
                Title = command.Request.Title,
                TypeId = command.Request.TypeId,
                Content = command.Request.Content,
                IsShow = command.Request.IsShow,
                State = command.Request.State
            });
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBlogInfoCommand command)
        {
            await _blogArticleRepository.RemoveAsync(command.Ids);
        }
    }
}
