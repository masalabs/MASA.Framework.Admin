namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public class EditPermissionCommandValidator : AbstractValidator<EditPermissionCommand>
{
    public EditPermissionCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.PermissionId)
            .NotEqual(Guid.Empty).WithMessage($"wrong {nameof(EditPermissionCommand.PermissionId)}");
        RuleFor(command => command.Name.Trim())
            .NotNull().WithMessage($"{nameof(EditPermissionCommand.Name)} name cannot be null")
            .MaximumLength(10).WithMessage($"{nameof(EditPermissionCommand.Name)} cannot exceed 10 characters")
            .MinimumLength(1).WithMessage($"{nameof(EditPermissionCommand.Name)} name cannot be empty");
    }
}
