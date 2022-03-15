namespace Masa.Framework.Admin.Service.Authentication.Domain.Services;

public class RoleDomainService : DomainService
{
    public RoleDomainService(IDomainEventBus eventBus) : base(eventBus)
    {
    }

    public async Task AddRoleAsync(Role role)
    {
        await EventBus.PublishAsync(
            new AddRoleDomainEvent(
                role.Id,
                role.Name,
                role.Description,
                role.Number,
                role.Enable,
                role.RoleItems.Select(role => role.RoleId).ToList()));
    }
}
