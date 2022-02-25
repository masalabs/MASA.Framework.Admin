namespace MASA.Framework.Sdks.Authentication.Request.Configuration.Menu;

public class DeleteMenuByIdsRequest
{
    public Guid[] MenuIds { get; set; }

    public DeleteMenuByIdsRequest(Guid[] menuIds)
    {
        MenuIds = menuIds;
    }
}
