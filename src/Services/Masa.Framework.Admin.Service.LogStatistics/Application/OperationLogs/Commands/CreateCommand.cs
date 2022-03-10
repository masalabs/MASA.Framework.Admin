using Masa.Framework.Sdks.Authentication.Request.LogStatistics;

namespace Masa.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands
{
    public record CreateCommand(OperationLogCreateRequest OperationLogCreateRequest) : CommandBase();
}
