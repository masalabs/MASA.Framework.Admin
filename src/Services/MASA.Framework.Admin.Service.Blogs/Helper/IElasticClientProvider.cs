using Nest;

namespace MASA.Framework.Admin.Service.Blogs.Helper
{
    public interface IElasticClientProvider
    {
        /// <summary>
        /// 指定index获取ElasticClient
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        ElasticClient GetClient(string indexName = "");
    }
}
