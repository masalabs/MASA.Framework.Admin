namespace MASA.Framework.Extensions.Swagger
{
    /// <summary>
    /// 枚举特性标记
    /// </summary>
    public class ApiGroupInfoAttribute : Attribute
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
