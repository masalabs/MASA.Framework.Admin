using MASA.Framework.Admin.Contracts.Base.Request;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands
{
    public record CreateCommand(OperationLogCreateRequest OperationLogCreateRequest) : CommandBase();
}
