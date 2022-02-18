namespace MASA.Framework.Admin.Configuration.Application.Menu;

public class CommandHandler
{
    private readonly IRepository<Domain.Aggregate.Menu> _repository;

    public CommandHandler(IRepository<Domain.Aggregate.Menu> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddAsync(MenuCommand.AddCommand command)
    {
        var menu = new Domain.Aggregate.Menu(
            command.Creator,
            command.Request.Name,
            command.Request.Code,
            command.Request.Url,
            command.Request.Sort,
            command.Request.Disabled)
        {
            Icon = command.Request.Icon,
            Describe = command.Request.Describe,
            ParentId = command.Request.ParentId,
            ParentName = command.Request.ParentName,
        };
        await _repository.AddAsync(menu);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task EditAsync(MenuCommand.EditCommand command)
    {
        var menu = await _repository.FindAsync(command.Request.MenuId);
        if (menu == null)
            throw new UserFriendlyException("the menu does not exist");

        menu.Update(command.Creator, command.Request.Name,command.Request.Url, command.Request.Sort,command.Request.Disabled);
        menu.Icon = command.Request.Icon;
        menu.Describe = command.Request.Describe;
        menu.ParentId = command.Request.ParentId;
        menu.ParentName = command.Request.ParentName;
        await _repository.UpdateAsync(menu);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task DeleteAsync(MenuCommand.DeleteCommand command)
    {
        await _repository.RemoveAsync(m => m.Id == command.request.MenuId);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}
