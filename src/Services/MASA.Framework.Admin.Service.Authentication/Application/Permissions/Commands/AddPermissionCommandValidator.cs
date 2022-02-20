namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public class AddPermissionCommandValidator : AbstractValidator<AddPermissionCommand>
{
    public AddPermissionCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.Name.Trim())
            .NotNull().WithMessage($"{nameof(AddPermissionCommand.Name)} name cannot be null")
            .MaximumLength(10).WithMessage($"{nameof(AddPermissionCommand.Name)} cannot exceed 10 characters")
            .MinimumLength(1).WithMessage($"{nameof(AddPermissionCommand.Name)} name cannot be empty");
        RuleFor(command => command.Resource)
            .Must(resource => !string.IsNullOrEmpty(resource) && resource.Length is >= 1 and <= 30)
            .WithMessage("Authorization information error");
        RuleFor(command => command.Scope)
            .Must(resource => !string.IsNullOrEmpty(resource) && resource.Length is >= 1 and <= 50)
            .WithMessage("Authorization information error");
        RuleFor(command => command.Action)
            .Must(resource => !string.IsNullOrEmpty(resource) && resource.Length is >= 1 and <= 100)
            .WithMessage("Authorization information error");
        RuleFor(command => command.PermissionType)
            .IsInEnum()
            .WithMessage("Unsupported grant type");
    }
}
