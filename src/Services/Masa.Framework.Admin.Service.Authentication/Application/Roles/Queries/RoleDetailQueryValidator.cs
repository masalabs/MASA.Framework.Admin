namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public class RoleDetailQueryValidator: AbstractValidator<RoleDetailQuery>
{
    public RoleDetailQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(query => query.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a RoleId");
    }
}
