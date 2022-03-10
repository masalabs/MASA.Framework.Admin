namespace Masa.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queries;

public record ListQuery(int PageIndex, int PageSize, string Description) : Query<PaginatedItemResponse<OperationLogItemResponse>>
{
    public override PaginatedItemResponse<OperationLogItemResponse> Result { get; set; } = null!;
}

