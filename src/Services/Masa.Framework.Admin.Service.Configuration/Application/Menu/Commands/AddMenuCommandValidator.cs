namespace Masa.Framework.Admin.Configuration.Application.Menu.Commands;

public class AddMenuCommandValidator : AbstractValidator<AddMenuCommand>
{
    public AddMenuCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.Name).NotNull().WithMessage($"The {nameof(AddMenuCommand.Name)} cannot be null");
        RuleFor(command => command.Code).NotNull().WithMessage($"The {nameof(AddMenuCommand.Code)} cannot be null");
    }
}
