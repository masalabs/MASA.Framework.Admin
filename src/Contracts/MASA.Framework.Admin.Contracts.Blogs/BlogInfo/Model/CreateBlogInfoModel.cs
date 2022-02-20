﻿namespace MASA.Framework.Admin.Contracts.Blogs;

public class CreateBlogInfoModel
{
    public Guid Id { get; set; }

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

    /// <summary>
    /// 创建人
    /// </summary>
    public Guid UserId { get; set; }
}