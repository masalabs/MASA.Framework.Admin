using System.ComponentModel;

namespace MASA.Framework.Admin.Contracts.Blogs
{
    public enum ReasonTypes
    {
        [Description("广告营销/虚假信息")]
        AdvertisingMarketing = 1,
        [Description("抄袭/侵权")]
        Tort,
        [Description("低俗")]
        Vulgar,
        [Description("政治敏感")]
        PoliticalSensitivity,
        [Description("骚扰谩骂")]
        HarassmentAndAbuse,
        [Description("病毒木马")]
        Virus,
        [Description("其他")]
        Other,
    }
}
