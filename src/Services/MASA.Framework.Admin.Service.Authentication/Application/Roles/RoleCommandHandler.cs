namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class RoleCommandHandler
{
    private readonly IRoleRepository _repository;

    public RoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddAsync(AddRoleCommand command)
    {
        if (await _repository.ExistAsync(command.Name))
            throw new UserFriendlyException("The current role already exists");

        var role = new Role(command.Creator, command.Name, command.Number);
        role.SetInheritedRole(command.ChildrenRoleIds);
        await _repository.AddAsync(role);
    }

    [EventHandler]
    public async Task EditAsync(EditRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        role.Update(command.Creator, command.Name, command.Describe);
        await _repository.UpdateAsync(role);
    }

    [EventHandler]
    public async Task DeleteAsync(DeleteRoleCommand command)
    {
        var role = await _repository.FindAsync(command.RoleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist");

        await _repository.RemoveAsync(role);
    }
}
