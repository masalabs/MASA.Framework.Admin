using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Utils.Configuration.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Infrastructure.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthOptions _authOptions;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
            _authOptions = AppSettings.GetModel<AuthOptions>("AuthOptions");
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Query["access_token"].FirstOrDefault()?.Split(" ").Last(); ;
            var token = accessToken ?? context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authOptions.Security);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    // 将clockskew设置为0，使令牌恰好在令牌到期时间到期(而不是5分钟后)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

                // attach user to context on successful jwt validation
                context.Items["UserId"] = userId;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
