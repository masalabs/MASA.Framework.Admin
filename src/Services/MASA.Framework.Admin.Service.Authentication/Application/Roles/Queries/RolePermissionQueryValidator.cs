namespace MASA.Framework.Admin.Service.Authentication.Application.Roles.Queries;

public class RolePermissionQueryValidator: AbstractValidator<RolePermissionQuery>
{
    public RolePermissionQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");
        RuleFor(query => query.RoleId)
            .NotEqual(Guid.Empty).WithMessage("Please select a RoleId");
    }
}
