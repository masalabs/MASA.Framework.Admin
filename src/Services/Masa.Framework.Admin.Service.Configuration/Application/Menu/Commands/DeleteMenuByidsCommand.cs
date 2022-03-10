namespace Masa.Framework.Admin.Configuration.Application.Menu.Commands;

public record DeleteMenuByIdsCommand(Guid[] MenuIds) : Command();

