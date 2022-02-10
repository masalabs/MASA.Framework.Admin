using MASA.Framework.Admin.Contracts.Order.Model;
using MASA.Framework.Admin.Service.Login.Infrastructure.Repositories;
using MASA.Framework.Admin.Service.Login.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;

namespace MASA.Framework.Admin.Service.Login.Hub
{
    [Authorize]
    public class LoginHub : Microsoft.AspNetCore.SignalR.Hub
    {
        [Inject]
        private LoginService LoginService { get; set; } = default!;

        [Inject]
        public IUserRepository UserRepository { get; set; } = default!;

        public override async Task OnConnectedAsync()
        {
            var userIdClaim = Context.User?.Claims.FirstOrDefault(claim => claim.Type == "UserId");

            if (!int.TryParse(userIdClaim?.Value, out int userId))
            {
                //SignalR链接异常，没有传递sub参数
            }
            else
            {
                OnlineUserModel onlineUser = await LoginService.GetOnlineUserByUserIdAsync(userId);

                if (onlineUser != null && !string.IsNullOrWhiteSpace(onlineUser.ConnectionId))
                {
                    string message = $"当前登录的账号已在其他客户端登录，您的登录已被注销。";
                    //send message to client
                    await Clients.Client(onlineUser.ConnectionId).SendAsync("LogOut", message);
                }

                if (onlineUser == null)
                {
                    UserModel userDTO = await UserRepository.GetUserAsync(userId);
                    onlineUser = new OnlineUserModel
                    {
                        UserId = userDTO.Id,
                        NickName = userDTO.NickName,
                        Account = userDTO.Account,
                    };
                }
                onlineUser.LoginTime = DateTime.Now;
                onlineUser.ConnectionId = Context.ConnectionId;

                await LoginService.AddOrUpdateOnlineUserAsync(onlineUser);

                //login success. 获取redis的在线用户数量，推送到前台
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            var userIdClaim = Context.User?.Claims.FirstOrDefault(claim => claim.Type == "sub");

            if (!int.TryParse(userIdClaim?.Value, out int userId))
            {
                Console.WriteLine($"断开记录用户在线的 SignalR 连接：用户于 {DateTime.Now:yyyy-MM-dd HH: mm:ss} 非正常退出登录,connectionId={connectionId}");
            }
            else
            {
                Console.WriteLine($"断开记录用户在线的 SignalR 连接：UserId={userId}的用户于 {DateTime.Now.ToString("yyyy-MM-dd HH: mm:ss")} 退出登录,connectionId={connectionId}");
                OnlineUserModel onlineUser = await LoginService.GetOnlineUserByUserIdAsync(userId);
                if (onlineUser != null && onlineUser.ConnectionId == connectionId)
                {
                    await LoginService.RemoveOnlineUserByUserIdAsync(userId);
                }
            }
        }
    }
}
