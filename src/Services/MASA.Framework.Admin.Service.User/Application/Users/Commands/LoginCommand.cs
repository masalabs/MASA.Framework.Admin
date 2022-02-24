namespace MASA.Framework.Admin.Service.User.Application.Users.Commands
{
    public record LoginCommand(UserLoginRequest UserLoginRequest) : Command
    {
        public string Token { get; set; } = "";
    }
}
