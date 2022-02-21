using MASA.Framework.Admin.Service.Api.Infrastructure.Entities;
using MASA.Framework.Admin.Service.Api.Infrastructure.Utils;

namespace MASA.Framework.Admin.Api
{
    public static class SeedData
    {
        public static async Task Initialize(this IHost host)
        {
            await using var scope = host.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AdminDbContext>();

            if (context.Users.Any())
            {
                return;
            }

            context.Add(new User
            {
                Account = "admin",
                NickName = "admin",
                Password = SHA1Utils.Encrypt($"admin123456"),
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now,
                Creator = Guid.NewGuid(),
                Modifier = Guid.NewGuid(),
                Salt = "123456",
                State = 1
            });

            context.SaveChanges();
        }
    }
}
