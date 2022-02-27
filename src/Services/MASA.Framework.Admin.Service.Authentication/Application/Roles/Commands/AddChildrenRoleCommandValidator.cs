namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Commands;

public class AddChildrenRoleCommandValidator : AbstractValidator<AddChildrenRolesCommand>
{
    public AddChildrenRoleCommandValidator()
    {
        RuleFor(command => command).NotNull().WithMessage($"Parameter error");        
    }
}
