namespace Masa.Framework.Admin.Service.User.Infrastructure.Options;

public class AppConfigOption : MasaConfigurationOptions
{
    public string DbConn { get; set; } = default!;

    /// <summary>
    /// Jwt认证Key
    /// </summary>
    public string Security { get; set; } = default!;

    /// <summary>
    /// 过期时间【天】
    /// </summary>
    public int Expiration { get; set; }

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "AppConfig";
    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}


