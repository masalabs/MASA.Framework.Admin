namespace MASA.Framework.Admin.Caller.Configurations;

public class CallerOptions : MasaConfigurationOptions
{
    public string ConfigurationCaller { get; set; } = default!;

    public string AuthenticationCaller { get; set; } = default!;

    public string UserCaller { get; set; } = default!;

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "ApiGateways";

    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}
