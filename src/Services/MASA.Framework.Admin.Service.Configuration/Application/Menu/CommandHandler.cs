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
            command.Request.Code,
            command.Request.Name,
            command.Request.Sort,
            command.Request.ParentId,
            command.Request.ParentName);
        await _repository.AddAsync(menu);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task EditAsync(MenuCommand.EditCommand command)
    {
        var menu = await _repository.FindAsync(command.Request.MenuId);
        if (menu == null)
            throw new UserFriendlyException("the menu does not exist");

        menu.Update(command.Creator, command.Request.Name, command.Request.Sort, command.Request.ParentId, command.Request.ParentName);
        await _repository.UpdateAsync(menu);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}
