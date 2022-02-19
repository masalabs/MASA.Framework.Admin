namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.Options;

public class DbConnectionOption : MasaConfigurationOptions
{
    public string DbConn { get; set; } = default!;

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "AppConfig";

    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}
