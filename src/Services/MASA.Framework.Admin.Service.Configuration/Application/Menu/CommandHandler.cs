namespace MASA.Framework.Admin.Configuration.Application.Menu;

public class CommandHandler
{
    private readonly IRepository<Domain.Aggregate.Menu> _repository;

    public CommandHandler(IRepository<Domain.Aggregate.Menu> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddAsync(MenuCommand.AddMenuCommand command)
    {
        var menu = new Domain.Aggregate.Menu(
            command.Creator,
            command.Code,
            command.Name,
            command.Url,
            command.Icon,
            command.Describe,
            command.ParentId,
            command.Sort,
            command.Disabled);
        await _repository.AddAsync(menu);
    }

    [EventHandler]
    public async Task EditAsync(MenuCommand.EditMenuCommand command)
    {
        var menu = await _repository.FindAsync(command.MenuId);
        if (menu == null)
            throw new UserFriendlyException("the menu does not exist");

        menu.Update(command.Creator,
            command.Name, command.Url,
            command.Icon,
            command.Describe,
            command.ParentId,
            command.Sort,
            command.Disabled);

        await _repository.UpdateAsync(menu);
    }

    [EventHandler]
    public async Task DeleteAsync(MenuCommand.DeleteMenuCommand command)
    {
        await _repository.RemoveAsync(m => m.Id == command.MenuId);
    }
}
