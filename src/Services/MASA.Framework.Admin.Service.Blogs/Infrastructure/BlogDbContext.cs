using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using MASA.Framework.Admin.Service.Blogs.Infrastructure.Entities;
using MASA.Utils.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure
{
    public class BlogDbContext : IntegrationEventLogContext
    {
        public DbSet<BlogAdvertisingPictures> BlogAdvertisingPictures { get; set; } = default!;
        public DbSet<BlogCommentInfo> BlogCommentInfoes { get; set; } = default!;
        public DbSet<BlogEnclosureInfo> BlogEnclosureInfoes { get; set; } = default!;
        public DbSet<BlogInfo> BlogInfoes { get; set; } = default!;
        public DbSet<BlogLabel> BlogLabels { get; set; } = default!;
        public DbSet<BlogType> BlogTypes { get; set; } = default!;

        public BlogDbContext(MasaDbContextOptions<BlogDbContext> options) : base(options)
        {
        }
    }
}
