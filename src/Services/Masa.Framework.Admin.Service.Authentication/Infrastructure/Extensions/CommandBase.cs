namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.Extensions;

public record CommandBase : Command
{
    [FromHeader(Name = "creator-id")]
    public Guid Creator { get; set; }
}
