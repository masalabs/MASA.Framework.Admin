using MASA.Framework.Sdks.Authentication.Request.LogStatistics;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands
{
    public record CreateCommand(OperationLogCreateRequest OperationLogCreateRequest) : CommandBase();
}
