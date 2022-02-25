namespace MASA.Framework.Admin.Configuration.Application.Menu.Commands;

public class DeleteMenuByIdsCommandValidator : AbstractValidator<DeleteMenuByIdsCommand>
{
    public DeleteMenuByIdsCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");
        RuleFor(command => command.MenuIds.Count()).GreaterThanOrEqualTo(1).WithMessage($"The {nameof(DeleteMenuByIdsCommand.MenuIds)} count must be greater than 0");
    }
}
