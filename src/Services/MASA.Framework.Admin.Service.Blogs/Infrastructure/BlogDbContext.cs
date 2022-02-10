using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure
{
    public class BlogDbContext : IntegrationEventLogContext
    {
        public DbSet<BlogAdvertisingPictures> BlogAdvertisingPictures { get; set; } 
        public DbSet<BlogCommentInfo> BlogCommentInfoes { get; set; }
        public DbSet<BlogInfo> BlogInfoes { get; set; }
        public DbSet<BlogLabel> BlogLabels { get; set; }
        public DbSet<BlogType> BlogTypes { get; set; }
        public DbSet<BlogLabelRelationship> BlogLabelRelationships { get; set; }
    }
}
