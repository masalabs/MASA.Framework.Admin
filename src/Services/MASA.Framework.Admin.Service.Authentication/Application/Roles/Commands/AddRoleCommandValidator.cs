namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(cmd => cmd.Request)
            .NotNull().WithMessage($"{nameof(AddRoleCommand.Request)} Can not be null");
        RuleFor(cmd => cmd.Request.Name.Trim())
            .MaximumLength(10).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Request.Name)}")
            .MinimumLength(1).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Request.Name)}");
        RuleFor(cmd => cmd.Request.Number)
            .Must(number => number > 0 || number == -1).WithMessage("The number of characters must be greater than 0 or -1");
    }
}
