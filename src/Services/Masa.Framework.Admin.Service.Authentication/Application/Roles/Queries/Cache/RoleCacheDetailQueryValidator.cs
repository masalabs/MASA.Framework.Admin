namespace Masa.Framework.Admin.Service.Authentication.Application.Roles.Queries.Cache;

public class RoleCacheDetailQueryValidator: AbstractValidator<RoleCacheDetailQuery>
{
    public RoleCacheDetailQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(query => query.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a RoleId");
    }
}
