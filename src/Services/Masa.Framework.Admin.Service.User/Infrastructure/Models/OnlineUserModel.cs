namespace Masa.Framework.Admin.Service.User.Infrastructure.Models;

public class OnlineUserModel
{
    public Guid UserId { get; set; }

    public string ConnectionId { get; set; } = "";

    public string Account { get; set; } = "";

    public string NickName { get; set; } = "";

    public DateTime LoginTime { get; set; }

    public string LoginIP { get; set; } = "";
}

