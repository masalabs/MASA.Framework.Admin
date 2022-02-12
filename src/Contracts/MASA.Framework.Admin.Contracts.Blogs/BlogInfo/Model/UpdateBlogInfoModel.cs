﻿namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class UpdateBlogInfoModel
    {
        /// <summary>
        /// 主键
        /// </summary>
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
        /// 删除标签列表
        /// </summary>
        public List<Guid> DeleteRelationIds { get; set; }

        /// <summary>
        /// 创建标签列表
        /// </summary>
        public List<string> AddLabels { get; set; }
    }
}