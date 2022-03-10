namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");

        RuleFor(command => command.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a role to edit");

        RuleFor(command => command.Name.Trim())
            .MaximumLength(10).WithMessage($"Please enter 1-10 digits of {nameof(EditRoleCommand.Name)}")
            .MinimumLength(1).WithMessage($"Please enter 1-10 digits of {nameof(EditRoleCommand.Name)}");
    }
}
