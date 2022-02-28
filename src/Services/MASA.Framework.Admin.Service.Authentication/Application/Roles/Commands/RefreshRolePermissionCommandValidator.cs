namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class RefreshRolePermissionCommandValidator : AbstractValidator<EditRoleCommand>
{
    public RefreshRolePermissionCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");

        RuleFor(command => command.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a role");
    }
}
