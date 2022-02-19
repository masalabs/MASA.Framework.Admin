namespace MASA.Framework.Admin.Contracts.Authentication.Request.Objects;

public class BatchDeleteObjectRequest
{
    public List<Guid> ObjectIds { get; set; }

    public BatchDeleteObjectRequest(List<Guid> objectIds)
    {
        ObjectIds = objectIds;
    }
}
