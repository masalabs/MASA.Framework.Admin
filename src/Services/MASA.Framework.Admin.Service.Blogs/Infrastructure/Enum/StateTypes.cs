using System.ComponentModel;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure.Enum
{
    public enum StateTypes
    {
        [Description("草稿")]
        Draft = 1,
        [Description("下架")]
        OffTheShelf,
        [Description("已审核")]
        Reviewed,
        [Description("待审核")]
        ToBeReviewed,
    }
}
