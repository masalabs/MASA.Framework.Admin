using MASA.Contrib.Service.MinimalAPIs;

namespace MASA.Framework.Admin.Service.Blogs.OpenServices
{
    public class TestService : ServiceBase
    {
        public TestService(IServiceCollection services) : base(services)
        {

            App.MapGet("get", () =>
            {
                return "123";

            });
        }

        public TestService(IServiceCollection services, string baseUri) : base(services, baseUri)
        {
        }



    }
}
