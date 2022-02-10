using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MASA.Framework.Extensions.Swagger
{
    /// <summary>
    /// swagger扩展
    /// </summary>
    public static class SwaggerGenServiceExtensions
    {
        /// <summary>
        /// swagger 文档扩展注册
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="groupTypes">Type</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenGroup(this IServiceCollection services, Type? groupTypes = null)
        {
            groupTypes = groupTypes ?? typeof(GroupTypes);

            services.AddSwaggerGen(c =>
            {
                //遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值

                groupTypes.GetFields().Skip(1).ToList().ForEach(fieldInfo =>
                {
                    var groupInfo = fieldInfo.GetCustomAttributes(typeof(ApiGroupInfoAttribute), false).OfType<ApiGroupInfoAttribute>().FirstOrDefault();

                    c.SwaggerDoc(fieldInfo.Name, new()
                    {
                        Title = groupInfo?.Title,
                        Version = groupInfo?.Version,
                        Description = groupInfo?.Description
                    });
                });

                c.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (docName == GroupTypes.Default.ToString())
                        return string.IsNullOrEmpty(apiDescription.GroupName);

                    return apiDescription.GroupName == docName;
                });

                // 解决相同类名会报错的问题
                c.CustomSchemaIds(type => type.FullName);
                Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.xml").ToList().ForEach(xml => c.IncludeXmlComments(xml, true));

            });

            return services;
        }

        /// <summary>
        /// swagger 文档扩展启用
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="groupTypes">Type</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerUiGroup(this IApplicationBuilder app, Type? groupTypes = null)
        {
            groupTypes = groupTypes ?? typeof(GroupTypes);
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/Default/swagger.json", "通用接口");

                //遍历ApiGroupNames所有枚举值生成接口文档，Skip(1)是因为Enum第一个FieldInfo是内置的一个Int值
                groupTypes.GetFields().Skip(1).ToList().ForEach(fieldInfo =>
                {
                    //获取枚举值上的特性
                    var groupInfo = fieldInfo.GetCustomAttributes(typeof(ApiGroupInfoAttribute), false).OfType<ApiGroupInfoAttribute>().FirstOrDefault();
                    var name = groupInfo != null ? groupInfo.Title : fieldInfo.Name;

                    c.SwaggerEndpoint($"/swagger/{fieldInfo.Name}/swagger.json", name);

                });

            });

            return app;
        }
    }
}
