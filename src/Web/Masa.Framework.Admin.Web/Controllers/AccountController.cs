namespace Masa.Framework.Admin.Web.Controllers;

[Microsoft.AspNetCore.Mvc.Route("[controller]/[action]")]
public class AccountController : Controller
{
    readonly UserCaller _userCaller;
    public ProtectedLocalStorage _protectedLocalStorage;

    public AccountController(UserCaller userCaller, ProtectedLocalStorage protectedLocalStorage)
    {
        _protectedLocalStorage = protectedLocalStorage;
        _userCaller = userCaller;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromServices] DaprClient daprClient, string token)
    {
        var SECRET_STORE_NAME = "localsecretstore";
        var secret = await daprClient.GetSecretAsync(SECRET_STORE_NAME, "jwt_security");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret["jwt_security"]);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            // 将clockskew设置为0，使令牌恰好在令牌到期时间到期(而不是5分钟后)
            ClockSkew = TimeSpan.Zero,
        }, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;
        var isAdmin = jwtToken.Claims.First(x => x.Type == "IsAdmin").Value;
        var permissions = await _userCaller.GetAuthorizeByUserAsync(Guid.Parse(userId));


        var claims = new List<Claim>
        {
            new Claim("UserId", userId),
            new Claim("Token",token),
            new Claim("Permissions",System.Text.Json.JsonSerializer.Serialize(permissions.Data)),
            new Claim("IsAdmin",isAdmin)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            RedirectUri = this.Request.Host.Value,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return LocalRedirect(Url.Content("~/"));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated == true)
        {
            // delete local authentication cookie
            await HttpContext.SignOutAsync();
        }

        return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
    }
}

