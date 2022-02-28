﻿namespace Masa.Framework.Admin.Configuration.Infrastructure.Options;

public class AppConfigOption : MasaConfigurationOptions
{
    public string DbConn { get; set; } = default!;

    public bool EnableDapr { get; set; }

    public override string? ParentSection { get; init; } = "Appsettings";

    public override string? Section { get; init; } = "AppConfig";

    public override SectionTypes SectionType { get; init; } = SectionTypes.Local;
}
