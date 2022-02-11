namespace MASA.Framework.Admin.Contracts.Base.Extensions.Configurations;

public class DbContextOptions : MasaConfigurationOptions
{
    public string DbConn { get; set; } = default!;

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "AppConfig";

    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}
