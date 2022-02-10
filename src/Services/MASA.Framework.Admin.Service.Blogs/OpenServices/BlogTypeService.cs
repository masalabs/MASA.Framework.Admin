using MASA.Contrib.Service.MinimalAPIs;
using MASA.Framework.Admin.Service.Blogs.Application.BlogTypes.Commands;
using MASA.Framework.Admin.Service.Blogs.Application.Commands;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class BlogTypeService : ServiceBase
    {
        public BlogTypeService(IServiceCollection services) : base(services)
        {
        }

        public BlogTypeService(IServiceCollection services, string baseUri) : base(services, baseUri)
        {
        }
        
        public async Task CreateAsync(CreateBlogTypeCommand command)
        {
           
        }
        
        public async Task UpdateAsync(UpdateBlogTypeCommand command)
        {
            
        }

        
        public async Task RemoveAsync(RemoveBolgTypeCommand command)
        {
            
        }

    }
}
