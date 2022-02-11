namespace MASA.Framework.Admin.Service.Blogs.Application.BlogTypes
{
    public class BlogTypeCommandHandlers
    {
        private readonly IBlogTypeRepository _blogTypeRepository;

        public BlogTypeCommandHandlers(IBlogTypeRepository blogTypeRepository)
        {
            this._blogTypeRepository = blogTypeRepository;
        }

        [EventHandler]
        public async Task CreateAsync(CreateBlogTypeCommand command)
        {
            await _blogTypeRepository.CreateAsync(new()
            {
                TypeName = command.Request.TypeName
            });
        }

        [EventHandler]
        public async Task UpdateAsync(UpdateBlogTypeCommand command)
        {
            await _blogTypeRepository.UpdateAsync(new()
            {
                Id = command.Request.Id,
                TypeName = command.Request.TypeName
            });
        }

        [EventHandler]
        public async Task RemoveAsync(RemoveBolgTypeCommand command)
        {
            await _blogTypeRepository.RemoveAsync(command.Ids);
        }
    }
}
