using MASA.Framework.Admin.Caller.Callers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MASA.Framework.Admin.Web.Global
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public readonly HttpClient _httpClient;

        public CustomAuthStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress ??= new Uri("http://localhost:6383");
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var aa = Thread.CurrentPrincipal;

                var result = await _httpClient.GetStringAsync("api/user/get");

                if (string.IsNullOrWhiteSpace(result))
                {
                    MarkUserAsLoggedOut();
                    return new AuthenticationState(new ClaimsPrincipal());
                }
                else
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, "admin"));
                    claims.Add(new Claim("UserId", result));
                    var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
                    return new AuthenticationState(authenticatedUser);
                }


            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var identity1 = new ClaimsIdentity(new Claim[]
                    {
                    }, "test");
                }
                return new AuthenticationState(new ClaimsPrincipal());
            }

            //var identity = new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, "admin"),
            //}, "Fake authentication type");

            //var user = new ClaimsPrincipal(identity);
            //return new AuthenticationState(user);
        }


        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public void MarkUserAsAuthenticated(string token)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "admin") });
            Thread.CurrentPrincipal = new ClaimsPrincipal(claimsIdentity);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            //此处应该根据服务器的返回的内容进行配置本地策略，作为演示，默认添加了“Admin”
            var claims = new List<Claim>();
            claims.Add(new Claim("UserId", "1"));
            claims.Add(new Claim(ClaimTypes.Name, "admin"));

            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// 标记注销
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            //HttpClient.DefaultRequestHeaders.Authorization = null;

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
