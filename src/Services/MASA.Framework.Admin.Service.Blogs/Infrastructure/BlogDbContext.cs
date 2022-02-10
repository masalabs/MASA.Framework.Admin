using System.Data.Common;
using MASA.BuildingBlocks.Data.UoW;
using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Utils.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure
{
    public class BlogDbContext : IntegrationEventLogContext
    {
        public DbSet<BlogAdvertisingPictures> BlogAdvertisingPictures { get; set; } 
        public DbSet<BlogCommentInfo> BlogCommentInfoes { get; set; }
        public DbSet<BlogEnclosureInfo> BlogEnclosureInfoes { get; set; } 
        public DbSet<BlogInfo> BlogInfoes { get; set; }
        public DbSet<Domain.Entities.BlogLabel> BlogLabels { get; set; }
        public DbSet<Domain.Entities.BlogType> BlogTypes { get; set; }
    }
}
