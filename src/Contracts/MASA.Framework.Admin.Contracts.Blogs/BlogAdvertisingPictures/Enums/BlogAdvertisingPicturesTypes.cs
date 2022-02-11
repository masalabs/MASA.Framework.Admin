using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Blogs.BlogAdvertisingPictures.Enums
{
    public enum BlogAdvertisingPicturesTypes : int
    {
        [Description("首页")]
        Home = 1,

        [Description("首页右下")]
        HomeLowerRight = 2,

        [Description("详情")]
        Details = 3,

        [Description("详情右下")]
        DetailsLowerRight = 4
    }
}
