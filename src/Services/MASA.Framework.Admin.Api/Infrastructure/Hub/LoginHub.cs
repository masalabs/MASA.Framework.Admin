﻿using MASA.Framework.Admin.Contracts.Login.Model;
using MASA.Framework.Admin.Service.Api.Infrastructure.Repositories;
using MASA.Framework.Admin.Service.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace MASA.Framework.Admin.Service.Infrastructure.Api.Hub
{
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

            if (!int.TryParse(userIdClaim?.Value, out int userId))
            {
                //SignalR链接异常，没有传递sub参数
            }
            else
            {
                OnlineUserModel? onlineUser = _loginService.GetOnlineUserByUserId(userId);

                if (onlineUser != null && !string.IsNullOrWhiteSpace(onlineUser.ConnectionId))
                {
                    string message = $"当前登录的账号已在其他客户端登录，您的登录已被注销。";
                    //send message to client
                    await Clients.Client(onlineUser.ConnectionId).SendAsync("Logout", message);
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

            if (!int.TryParse(userIdClaim?.Value, out int userId))
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
}