using System.Data.Common;
using MASA.BuildingBlocks.Data.UoW;
using MASA.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using MASA.Framework.Admin.Service.Blogs.Domain.Entities;
using MASA.Utils.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Blogs.Infrastructure
{
    public class BlogDbContext : IntegrationEventLogContext, IUnitOfWork
    {
        public DbSet<BlogAdvertisingPictures> BlogAdvertisingPictures { get; set; } 
        public DbSet<BlogCommentInfo> BlogCommentInfoes { get; set; }
        public DbSet<BlogEnclosureInfo> BlogEnclosureInfoes { get; set; } 
        public DbSet<BlogInfo> BlogInfoes { get; set; }
        public DbSet<BlogLabel> BlogLabels { get; set; }
        public DbSet<BlogType> BlogTypes { get; set; } 
        public DbSet<BlogApprovedRecord> BlogApprovedRecords { get; set; }
        public DbSet<BlogLabelRelationship> BlogLabelRelationships { get; set; }

        public BlogDbContext(MasaDbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
           return base.SaveChangesAsync();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public DbTransaction Transaction { get; }
        public bool TransactionHasBegun { get; }
        public bool UseTransaction { get; set; }
        public bool DisableRollbackOnFailure { get; set; }
    }
}
