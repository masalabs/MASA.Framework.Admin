namespace MASA.Framework.Sdks.Authentication.Callers
{
    public class UserGroupCaller : CallerBase
    {

        protected override string BaseAddress { get; set; } = "";

        public UserGroupCaller(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            Name = nameof(UserGroupCaller);
            BaseAddress = configuration["ApiGateways:UserCaller"];
        }

        protected override IHttpClientBuilder UseHttpClient()
        {
            return base.UseHttpClient().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }

    }
}
