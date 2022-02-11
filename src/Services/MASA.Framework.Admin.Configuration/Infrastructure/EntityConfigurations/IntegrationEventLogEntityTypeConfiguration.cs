namespace MASA.Framework.Admin.Configuration.Infrastructure.EntityConfigurations;

public class IntegrationEventLogEntityTypeConfiguration
    : IEntityTypeConfiguration<IntegrationEventLog>
{
    public void Configure(EntityTypeBuilder<IntegrationEventLog> builder)
    {
        builder.ToTable("integration_event_log", ConfigurationDbContext.DEFAULT_SCHEMA);
        builder.HasKey(((Expression<Func<IntegrationEventLog, object>>)(e => e.Id))!);
        builder.Property(e => e.Id).IsRequired(true);
        builder.Property(e => e.Content).IsRequired(true);
        builder.Property(e => e.CreationTime).IsRequired(true);
        builder.Property(e => e.State)
            .IsRequired(true);
        builder.Property(e => e.TimesSent).IsRequired(true);
        builder.Property(e => e.EventTypeName).IsRequired(true);
    }
}
