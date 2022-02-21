namespace MASA.Framework.Admin.Service.User.Infrastructure.Options;

public class AppConfigOption : MasaConfigurationOptions
{
    public string DbConn { get; set; } = default!;

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "AppConfig";
    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}


