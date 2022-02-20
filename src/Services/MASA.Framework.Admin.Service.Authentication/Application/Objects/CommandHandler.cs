namespace MASA.Framework.Admin.Service.Authentication.Application.Objects;

public class CommandHandler
{
    private readonly IObjectRepository _repository;

    public CommandHandler(IObjectRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddObject(ObjectCommand.AddCommand command)
    {
        if (await _repository.ExistAsync(command.Request.Code))
            throw new UserFriendlyException("duplicate resource");

        var objectItem = new Domain.Aggregate.ObjectAggregate.Object(command.Creator, command.Request.Code, command.Request.Name,
            command.Request.ObjectType);
        command.Request.ObjectItems.ForEach(permission =>
        {
            objectItem.AddPermission(permission.Name, permission.ObjectIdentifies, permission.Action, permission.PermissionType);
        });
        await _repository.AddAsync(objectItem);
        await _repository.UnitOfWork.SaveChangesAsync();
    }

    [EventHandler]
    public async Task EditObject(ObjectCommand.EditCommand command)
    {
        var objectItem = await _repository.FindAsync(command.Request.ObjectId);
        if (objectItem == null)
            throw new UserFriendlyException("The current object does not exist", Code.NOT_FIND_ERROR);

        objectItem.Update(command.Request.Name);
        await _repository.UpdateAsync(objectItem);
        await _repository.UnitOfWork.SaveChangesAsync();
    }
}
