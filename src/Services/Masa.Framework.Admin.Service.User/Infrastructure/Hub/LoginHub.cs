namespace Masa.Framework.Admin.Service.User.Infrastructure.Hub;

[Authorize]
public class LoginHub : Microsoft.AspNetCore.SignalR.Hub
{
    private static List<OnlineUserModel> _onlineUsers = new List<OnlineUserModel>();
    private readonly LoginService _loginService;

    public IUserRepository UserRepository;

    public LoginHub(IUserRepository userRepository, IMemoryCache memoryCache)
    {
        _loginService = new LoginService(memoryCache);
        UserRepository = userRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var userIdClaim = Context.User?.Claims.FirstOrDefault(claim => claim.Type == "UserId");
        var sessionId = Context.User?.Claims.FirstOrDefault(claim => claim.Type == "SessionId");

        if (!Guid.TryParse(userIdClaim?.Value, out Guid userId))
        {
            //SignalR链接异常，没有传递sub参数
        }
        else
        {
            OnlineUserModel? onlineUser = _loginService.GetOnlineUserByUserId(userId);

            if (onlineUser != null && !string.IsNullOrWhiteSpace(onlineUser.ConnectionId) && onlineUser.SessionId != sessionId?.Value)
            {
                string message = $"当前登录的账号已在其他客户端登录，您的登录已被注销。";
                //send message to client
                await Clients.Client(onlineUser.ConnectionId).SendAsync("Logout", message);
            }

            if (onlineUser == null)
            {
                var user = await UserRepository.GetByIdAsync(userId);
                if (user is null)
                {
                    return;
                }
                onlineUser = new OnlineUserModel
                {
                    UserId = user.Id,
                    NickName = user.Name,
                    Account = user.Account,
                    SessionId = sessionId?.Value
                };
            }
            onlineUser.LoginTime = DateTime.Now;
            onlineUser.ConnectionId = Context.ConnectionId;

            var removeUser = _onlineUsers.FirstOrDefault(u => u.UserId == userId);
            if (removeUser != null)
            {
                _onlineUsers.Remove(removeUser);
            }
            _onlineUsers.Add(onlineUser);
            _loginService.AddOrUpdateOnlineUsers(_onlineUsers);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var userIdClaim = Context.User?.Claims.FirstOrDefault(claim => claim.Type == "sub");

        if (!Guid.TryParse(userIdClaim?.Value, out Guid userId))
        {
            Console.WriteLine($"断开记录用户在线的 SignalR 连接：用户于 {DateTime.Now:yyyy-MM-dd HH: mm:ss} 非正常退出登录,connectionId={connectionId}");
        }
        else
        {
            Console.WriteLine($"断开记录用户在线的 SignalR 连接：UserId={userId}的用户于 {DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss")} 退出登录,connectionId={connectionId}");
            OnlineUserModel? onlineUser = _loginService.GetOnlineUserByUserId(userId);
            if (onlineUser != null && onlineUser.ConnectionId == connectionId)
            {
                _onlineUsers.Remove(onlineUser);
                _loginService.AddOrUpdateOnlineUsers(_onlineUsers);
            }
        }

        return base.OnDisconnectedAsync(exception);
    }
}

