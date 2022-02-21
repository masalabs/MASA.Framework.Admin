using MASA.Framework.Admin.Service.Api.Infrastructure.Entities;

namespace MASA.Framework.Admin.Api
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetService<AdminDbContext>();

            if (context.Users.Any())
            {
                return;
            }

            context.Add(new User
            {
                NickName = "zhansan",
                Password = "123456"
            });
            context.SaveChanges();
        }
    }
}
