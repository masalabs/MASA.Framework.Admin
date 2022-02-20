namespace MASA.Framework.Admin.Service.Authentication.Application.Objects.Commands;

public record AddObjectCommand(
    string Code,
    string Name,
    ObjectType ObjectType) : CommandBase;
