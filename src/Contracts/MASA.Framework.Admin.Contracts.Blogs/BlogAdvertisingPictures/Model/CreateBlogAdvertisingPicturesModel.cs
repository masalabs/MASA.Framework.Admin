﻿using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class CreateBlogAdvertisingPicturesModel
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Required]
        public string Pic { get; set; }

        /// <summary>
        /// 类型：1 首页 2首页右下 3详情 4详情右下
        /// </summary>
        [NonDefault]
        public BlogAdvertisingPicturesTypes Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态：true启用 false停用
        /// </summary>
        public bool Status { get; set; }
    }
}