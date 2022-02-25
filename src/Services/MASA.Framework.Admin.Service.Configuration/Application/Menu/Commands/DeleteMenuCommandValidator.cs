namespace MASA.Framework.Admin.Configuration.Application.Menu.Commands;

public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
{
    public DeleteMenuCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.MenuId).NotEqual(Guid.Empty).WithMessage($"wrong {nameof(DeleteMenuCommand.MenuId)}");
    }
}
