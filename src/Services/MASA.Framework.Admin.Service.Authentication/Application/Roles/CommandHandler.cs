namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class CommandHandler
{
    private readonly IRoleRepository _repository;

    public CommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddAsync(RoleCommand.AddCommand command)
    {
        if (await _repository.ExistAsync(command.Request.Name))
            throw new UserFriendlyException("The current role already exists", Code.REPEAT_ERROR);

        var role = new Role(command.Creator, command.Request.Name, command.Request.Number, command.Request.State,command.Request.Describe);
        role.SetInheritedRole(command.Request.ChildrenIds);
        await _repository.AddAsync(role);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task EditAsync(RoleCommand.EditCommand command)
    {
        var role = await _repository.FindAsync(command.Request.RuleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist", Code.NOT_FIND_ERROR);

        role.Update(command.Creator, command.Request.Name, command.Request.Describe,command.Request.Number,command.Request.State);
        await _repository.UpdateAsync(role);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task DeleteAsync(RoleCommand.DeleteCommand command)
    {        
        await _repository.RemoveAsync(r => r.Id == command.Request.RuleId);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}
