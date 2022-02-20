namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public class RoleBaseQueryValidator : AbstractValidator<RoleBaseQuery>
{
    public RoleBaseQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(query => query.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a RoleId");
    }
}
