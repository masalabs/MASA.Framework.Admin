﻿using MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums;

namespace MASA.Framework.Admin.Contracts.Blogs
{
    public class GetBlogAdvertisingPicturesFrontOption
    {
        /// <summary>
        /// 类型：1 首页 2首页右下 3详情 4详情右下
        /// </summary>
        public List<BlogAdvertisingPicturesTypes> Types { get; set; }
    }
}
