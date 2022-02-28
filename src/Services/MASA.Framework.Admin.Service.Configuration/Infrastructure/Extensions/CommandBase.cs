namespace Masa.Framework.Admin.Configuration.Infrastructure.Extensions;

public record CommandBase : Command
{
    [FromHeader(Name = "creator-id")]
    public Guid Creator { get; set; }
}
