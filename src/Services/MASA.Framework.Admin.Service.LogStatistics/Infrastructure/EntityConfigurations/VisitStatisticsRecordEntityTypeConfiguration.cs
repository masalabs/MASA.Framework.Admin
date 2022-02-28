using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.EntityConfigurations
{
    public class VisitStatisticsRecordEntityTypeConfiguration : IEntityTypeConfiguration<VisitStatisticsRecord>
    {
        public void Configure(EntityTypeBuilder<VisitStatisticsRecord> builder)
        {
            builder.ToTable("visit_statistics_record", LogStatisticsDbContext.DEFAULT_SCHEMA);

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id").IsRequired();

            builder.Property(s => s.IPCount).HasColumnName("ip_count").HasMaxLength(20).IsRequired();
            builder.Property(s => s.PV).HasColumnName("pv").HasDefaultValue(0).IsRequired();
            builder.Property(s => s.UV).HasColumnName("uv").HasDefaultValue(0).IsRequired();
            builder.Property(s => s.Type).HasColumnName("typc").IsRequired();
        }
    }
}
