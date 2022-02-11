﻿namespace MASA.Framework.Admin.Service.Authentication.Application.Roles;

public class CommandHandler
{
    private readonly IRoleRepository _repository;

    public CommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddRoleAsync(AddRoleCommand command)
    {
        if (await _repository.ExistAsync(command.Request.Name))
            throw new UserFriendlyException("The current role already exists", Code.REPEAT_ERROR);

        var role = new Role(command.UserId, command.Request.Name, command.Request.Number);
        role.SetInheritedRole(command.Request.ChildrenIds);
        await _repository.AddAsync(role);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task EditRoleAsync(EditRoleCommand command)
    {
        throw new UserFriendlyException("asd");
        var role = await _repository.FindAsync(command.Request.RuleId);
        if (role == null)
            throw new UserFriendlyException("The current role does not exist", Code.NOT_FIND_ERROR);

        role.Update(command.Request.Name, command.Request.Describe);
        await _repository.UpdateAsync(role);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}
