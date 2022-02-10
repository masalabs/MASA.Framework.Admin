using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Service.Order.Infrastructure.Filters
{
    public class ApiAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["UserId"];
            //验证是否需要授权和授权信息
            if (HasAllowAnonymous(context) == false && user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            if (filters.OfType<IAllowAnonymousFilter>().Any())
            {
                return true;
            }

            // When doing endpoint routing, MVC does not add AllowAnonymousFilters for AllowAnonymousAttributes that
            // were discovered on controllers and actions. To maintain compat with 2.x,
            // we'll check for the presence of IAllowAnonymous in endpoint metadata.
            var endpoint = context.HttpContext.GetEndpoint();
            return endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;
        }
    }
}
