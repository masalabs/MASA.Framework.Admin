namespace MASA.Framework.Admin.Service.Blogs.Model
{
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

    public class BlogAppSettiings
    { 
        public ElasticConfig ElasticConfig { get; set; }
    }
}
