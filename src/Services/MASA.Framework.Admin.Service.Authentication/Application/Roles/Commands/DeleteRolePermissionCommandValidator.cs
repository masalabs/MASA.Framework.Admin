namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class DeleteRolePermissionCommandValidator : AbstractValidator<DeleteRolePermissionCommand>
{
    public DeleteRolePermissionCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");

        RuleFor(command => command.Creator)
            .NotEqual(Guid.Empty).WithMessage($"{nameof(AddRolePermissionDomainCommand.Creator)} cannot be empty");

        RuleFor(command => command.RoleId)
            .NotEqual(Guid.Empty).WithMessage($"{nameof(AddRolePermissionDomainCommand.RoleId)} cannot be empty");

        RuleFor(command => command.PermissionId)
            .NotEqual(Guid.Empty).WithMessage($"{nameof(AddRolePermissionDomainCommand.PermissionId)} cannot be empty");
    }
}
