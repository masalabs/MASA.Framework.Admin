using MASA.Contrib.Dispatcher.Events;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Framework.Admin.Service.Blogs.Domain.IRepositorys;

namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes
{
    public class BlogTypeCommandHandler
    {
        private readonly IBlogTypeRepository _blogTypeRepository;

        public BlogTypeCommandHandler(IBlogTypeRepository blogTypeRepository)
        {
            this._blogTypeRepository = blogTypeRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogTypeCommand command)
        {
            await _blogTypeRepository.CreateAsync(new()
            {
                TypeName = command.TypeName
            });
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogTypeCommand command)
        {
            await _blogTypeRepository.UpdateAsync(new()
            {
                Id = command.Id,
                TypeName = command.TypeName
            });
        }


        [EventHandler]
        public async Task RemoveAsync(RemoveBolgTypeCommand command)
        {
            await _blogTypeRepository.RemoveAsync(command.Ids);
        }
    }
}
