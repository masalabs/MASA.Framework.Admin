namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.RoleId)
            .NotEqual(Guid.Empty).WithMessage($"wrong {nameof(DeleteRoleCommand.RoleId)}");
    }
}
