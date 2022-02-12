namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class BlogAppSettiings
    { 
        public ElasticConfig ElasticConfig { get; set; }

        public BaiduAIConfig BaiduAIConfig { get; set; }
    }

    public class ElasticConfig
    {
        /// <summary>
        /// 连接串
        /// </summary>
        public string Urls { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string IndexPrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 超时时间（毫秒）
        /// </summary>
        public int RequestTimeout { get; set; }
    }

    public class BaiduAIConfig
    { 
        public string GetAccessTokenUrl { get; set; }

        public string PostUserDefined { get; set; }

        public string APIKey { get; set; }

        public string SecretKey { get; set; }
    }
}
