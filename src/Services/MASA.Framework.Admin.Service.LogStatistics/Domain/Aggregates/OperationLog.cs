using MASA.BuildingBlocks.DDD.Domain.Entities.Auditing;
using MASA.Framework.Admin.Contracts.Base.Enum;

namespace MASA.Framework.Admin.Service.LogStatistics.Domain.Aggregates
{
    public class OperationLog : AuditAggregateRoot<Guid, Guid>
    {
        public string Description { get; private set; }

        public OperationLogType Type { get; private set; }

        public string ClientIP { get; private set; }

        public Guid UserId { get; private set; }

        public OperationLog(Guid userId, OperationLogType type, string description)
        {
            ClientIP = HttpContext.Connection.RemoteIpAddress.ToString();
            Description = description;
            Type = type;
            UserId = userId;
        }
    }
}
