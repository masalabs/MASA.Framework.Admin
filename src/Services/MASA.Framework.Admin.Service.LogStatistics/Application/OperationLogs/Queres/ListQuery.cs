using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Base.Response;
using MASA.Framework.Admin.Infrastructure.Configurations.Response;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queres
{
    public record ListQuery(int PageIndex, int PageSize, string Description) : Query<PaginatedItemResponse<OperationLogItemResponse>>
    {
        public override PaginatedItemResponse<OperationLogItemResponse> Result { get; set; } = null!;
    }
}
