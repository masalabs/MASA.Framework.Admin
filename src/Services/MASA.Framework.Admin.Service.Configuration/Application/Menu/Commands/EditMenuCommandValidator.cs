namespace MASA.Framework.Admin.Configuration.Application.Menu.Commands;

public class EditMenuCommandValidator : AbstractValidator<EditMenuCommand>
{
    public EditMenuCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.MenuId).NotEqual(Guid.Empty).WithMessage($"wrong {nameof(EditMenuCommand.MenuId)}");
        RuleFor(command => command.Name).NotNull().WithMessage($"The {nameof(EditMenuCommand.Name)} cannot be null");
    }
}
