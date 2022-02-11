namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(cmd => cmd.Request)
            .NotNull().WithMessage($"{nameof(AddRoleCommand.Request)} Can not be null");

        RuleFor(cmd => cmd.Request.RuleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a role to edit");

        RuleFor(cmd => cmd.Request.Name.Trim())
            .MaximumLength(10).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Request.Name)}")
            .MinimumLength(1).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Request.Name)}");
    }
}
