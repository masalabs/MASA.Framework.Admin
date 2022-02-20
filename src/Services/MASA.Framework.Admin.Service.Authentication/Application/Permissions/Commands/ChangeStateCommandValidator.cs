namespace MASA.Framework.Admin.Service.Authentication.Application.Permissions.Commands;

public class ChangeStateCommandValidator : AbstractValidator<ChangeStateCommand>
{
    public ChangeStateCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.PermissionId)
            .NotEqual(Guid.Empty).WithMessage($"wrong {nameof(EditPermissionCommand.PermissionId)}");
    }
}
