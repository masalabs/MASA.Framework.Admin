using MASA.Framework.Admin.Service.Blogs.Infrastructure.Enum;

namespace MASA.Framework.Admin.Service.Blogs.Model.BlogInfo.Model;

public class BlogInfoAddModel
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

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
    /// 是否保存为草稿
    /// </summary>
    public bool IsDraft { get; set; }

    /// <summary>
    /// 文章状态
    /// </summary>
    public StateTypes State { get; set; }
}
