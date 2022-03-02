namespace Masa.Framework.Admin.Service.LogStatistics.Infrastructure.EntityConfigurations;

public class OperationLogEntityTypeConfiguration : IEntityTypeConfiguration<OperationLog>
{
    public void Configure(EntityTypeBuilder<OperationLog> builder)
    {
        builder.ToTable("operation_log", LogStatisticsDbContext.DEFAULT_SCHEMA);

        builder.HasKey(log => log.Id);
        builder.Property(log => log.Id).HasColumnName("id").IsRequired();

        builder.Property(log => log.Type).HasColumnName("type").IsRequired();
        builder.Property(log => log.Description).HasColumnName("description");
        builder.Property(log => log.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(log => log.ClientIP).HasColumnName("client_ip").HasDefaultValue("").IsRequired();

        builder.Property(log => log.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(log => log.Creator).HasColumnName("creator").IsRequired();
        builder.Property(log => log.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(log => log.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(log => log.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}

