namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Queres;

public record ListQuery(int PageIndex, int PageSize, string Description) : Query<PaginatedItemResponse<OperationLogItemResponse>>
{
    public override PaginatedItemResponse<OperationLogItemResponse> Result { get; set; } = null!;
}

