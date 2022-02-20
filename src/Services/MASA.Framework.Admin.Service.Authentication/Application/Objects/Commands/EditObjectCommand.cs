namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Commands;

public record EditObjectCommand(Guid ObjectId, string Name) : CommandBase;
