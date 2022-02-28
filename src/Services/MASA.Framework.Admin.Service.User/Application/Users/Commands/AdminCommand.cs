namespace Masa.Framework.Admin.Service.User.Application.Users.Commands;

public record AdminCommand : Command
{
    public Guid Creator { get; set; }
}

