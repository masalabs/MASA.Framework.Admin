using MASA.Framework.Admin.Service.Blogs.Infrastructure.Enum;

namespace MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Model;

public class CreateBlogInfoModel
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 类型编号
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否仅自己可见
    /// </summary>
    public bool IsShow { get; set; }

    /// <summary>
    /// 文章状态
    /// </summary>
    public StateTypes State { get; set; }

    /// <summary>
    /// 标签列表
    /// </summary>
    public List<string> Labels { get; set; }
}
