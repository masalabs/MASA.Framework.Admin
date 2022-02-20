namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class ObjectEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.Aggregates.ObjectAggregate.Object>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.ObjectAggregate.Object> builder)
    {
        builder.ToTable("resources", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.Code).HasColumnName("code").HasMaxLength(20).IsRequired();
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(c => c.ObjectType).HasColumnName("object_type").IsRequired();
        builder.Property(c => c.Enable).HasColumnName("enable").IsRequired();
        builder.Property(c => c.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(c => c.Creator).HasColumnName("creator").IsRequired();
        builder.Property(c => c.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(c => c.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(c => c.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}
