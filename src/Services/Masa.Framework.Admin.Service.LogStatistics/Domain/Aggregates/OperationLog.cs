namespace Masa.Framework.Admin.Service.LogStatistics.Domain.Aggregates;

public class OperationLog : AuditAggregateRoot<Guid, Guid>, ISoftDelete
{
    public string Description { get; private set; }

    public OperationLogType Type { get; private set; }

    public string ClientIP { get; private set; }

    public Guid UserId { get; private set; }

    public OperationLog(Guid userId, OperationLogType type, string description)
    {
        ClientIP = "";
        Description = description;
        Type = type;
        UserId = userId;
    }
}

