using MASA.BuildingBlocks.Data.UoW;
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

            for (int i = 0; i < 10; i++)
            {
                var account = $"admin{i}";
                context.Add(new User
                {
                    Account = account,
                    NickName = account,
                    Password = SHA1Utils.Encrypt($"{account}123456"),
                    CreationTime = DateTime.Now,
                    ModificationTime = DateTime.Now,
                    Creator = Guid.NewGuid(),
                    Modifier = Guid.NewGuid(),
                    Salt = "123456",
                    State = 1
                });
            }

            context.SaveChanges();

            var unitOfWork = services.GetRequiredService<IUnitOfWork>();
            unitOfWork.CommitAsync().Wait();
        }
    }
}
