using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;

namespace MASA.Framework.Admin.Service.Blogs.Helper
{
    public class ElasticClientProvider : IElasticClientProvider
    {
        private readonly BlogAppSettiings _settings;
        public ElasticClientProvider(IOptions<BlogAppSettiings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public ElasticClient GetClient(string indexName = "")
        {
            var urls = _settings.ElasticConfig.Urls.Split(';').Select(t => new Uri(t));
            var esSettings = urls.Count() == 1 ?
                new ConnectionSettings(urls.First())
                    .BasicAuthentication(_settings.ElasticConfig.User,
                    _settings.ElasticConfig.Password)
                    .RequestTimeout(TimeSpan.FromMilliseconds(_settings.ElasticConfig.RequestTimeout)) :
                new ConnectionSettings(new SniffingConnectionPool(urls))
                    .BasicAuthentication(_settings.ElasticConfig.User,
                    _settings.ElasticConfig.Password)
                    .RequestTimeout(TimeSpan.FromMilliseconds(_settings.ElasticConfig.RequestTimeout));

            if (!string.IsNullOrWhiteSpace(indexName))
                esSettings.DefaultIndex(indexName);

            return new ElasticClient(esSettings);
        }
    }
}
