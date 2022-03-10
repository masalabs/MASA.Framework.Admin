namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public class PermissionListQueryValidator : AbstractValidator<PermissionListQuery>
{
    public PermissionListQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");

        RuleFor(query => query.State)
            .Must(state => state == -1 || state == 0 || state == 1)
            .WithMessage("Unsupported state type");
    }

}
