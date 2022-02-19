namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(command => command)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.Name.Trim())
            .MaximumLength(10).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Name)}")
            .MinimumLength(1).WithMessage($"Please enter 1-10 digits of {nameof(AddRoleCommand.Name)}");
        RuleFor(command => command.Number)
            .Must(number => number > 0 || number == -1).WithMessage("The number of characters must be greater than 0 or -1");
    }
}
