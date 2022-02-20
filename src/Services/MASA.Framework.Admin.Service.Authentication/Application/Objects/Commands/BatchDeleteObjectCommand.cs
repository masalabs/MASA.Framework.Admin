namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Commands;

public record BatchDeleteObjectCommand(List<Guid> ObjectIds) : CommandBase;
