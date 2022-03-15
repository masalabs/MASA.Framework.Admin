namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public class PermissionListQueryValidator : AbstractValidator<PermissionListQuery>
{
    public PermissionListQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");
    }

}
