namespace Masa.Framework.Admin.Service.Authentication.Application.Permissions.Queries;

public class PermissionDetailQueryValidator : AbstractValidator<PermissionDetailQuery>
{
    public PermissionDetailQueryValidator()
    {
        RuleFor(query => query)
            .NotNull().WithMessage($"Parameter error");

        RuleFor(query => query.PermissionId)
            .NotEqual(Guid.Empty).WithMessage("Please select a permission id");
    }

}
