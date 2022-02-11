namespace MASA.Framework.Admin.Contracts.Base.Commands;

public record Command : ICommand
{
    public Guid Id { get; }

    public DateTime CreationTime { get; }

    [JsonIgnore]
    public IUnitOfWork? UnitOfWork { get; set; }
}
