namespace MASA.Framework.Admin.Configuration.Application.Menu.Commands;

public record DeleteMenuByidsCommand(Guid[] MenuIds) : Command();

