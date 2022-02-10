using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MASA.Framework.Extensions.Swagger
{
    /// <summary>
    /// Api接口分组特性标签
    /// </summary>
    public class ApiGroupAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 组别名称
        /// </summary>
        public string? GroupName { get; set; } = String.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiGroupAttribute(string template = "[action]") : base($"/api/[controller]/{template}") { }

        /// <summary>
        /// 按GroupName分组
        /// </summary>
        /// <param name="groupName">分组枚举</param>
        /// <param name="template"></param>
        public ApiGroupAttribute(object groupName, string template = "[action]") : base($"/api/{groupName.ToString()}/[controller]/{template}")
        {
            GroupName = groupName.ToString();
        }

        /// <summary>
        /// 按GroupName分组,再按版本号区分路由，
        /// 适用于API网关转发
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="versionTypes"></param>
        /// <param name="template"></param>
        public ApiGroupAttribute(object groupName, VersionTypes versionTypes, string template = "[action]") : base($"/api/{versionTypes.ToString()}/{groupName.ToString()}/[controller]/{template}")
        {
            GroupName = groupName.ToString();
        }

        /// <summary>
        /// 不区分组别按版本号定义路由
        /// </summary>
        /// <param name="versionTypes"></param>
        /// <param name="template"></param>
        public ApiGroupAttribute(VersionTypes versionTypes, string template = "[action]") : base($"/api/{versionTypes.ToString()}/[controller]/{template}") { }
    }
}
