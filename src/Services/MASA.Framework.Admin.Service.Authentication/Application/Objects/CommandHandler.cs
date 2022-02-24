namespace MASA.Framework.Admin.Service.Authentication.Application.Objects;

public class CommandHandler
{
    private readonly IRepository<Domain.Aggregates.ObjectAggregate.Object> _repository;

    public CommandHandler(IRepository<Domain.Aggregates.ObjectAggregate.Object> repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task AddObjectAsync(AddObjectCommand command)
    {
        if (await _repository.GetCountAsync(obj => obj.Code == command.Code)>1)
            throw new UserFriendlyException("duplicate resource");

        var objectItem = new Domain.Aggregates.ObjectAggregate.Object(
            command.Creator,
            command.Code,
            command.Name,
            command.ObjectType);
        await _repository.AddAsync(objectItem);
    }

    [EventHandler]
    public async Task EditObjectAsync(EditObjectCommand command)
    {
        var objectItem = await _repository.FindAsync(command.ObjectId);
        if (objectItem == null)
            throw new UserFriendlyException("The current object does not exist");

        objectItem.Update(command.Name);
        await _repository.UpdateAsync(objectItem);
    }

    [EventHandler]
    public async Task DeleteObjectAsync(DeleteObjectCommand command)
    {
        await _repository.RemoveAsync(o => o.Id == command.ObjectId);
    }

    [EventHandler]
    public async Task BatchDeleteAsync(BatchDeleteObjectCommand command)
    {
        await _repository.RemoveAsync(o => command.ObjectIds.Contains(o.Id));
    }
}
