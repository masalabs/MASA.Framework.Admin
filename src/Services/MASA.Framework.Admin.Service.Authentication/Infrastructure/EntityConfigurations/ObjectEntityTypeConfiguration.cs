namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class ObjectEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.Aggregates.ObjectAggregate.Object>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.ObjectAggregate.Object> builder)
    {
        builder.ToTable("resources", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(obj => obj.Id);
        builder.Property(obj => obj.Id).HasColumnName("id").IsRequired();

        builder.Property(obj => obj.Code).HasColumnName("code").HasMaxLength(20).IsRequired();
        builder.Property(obj => obj.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(obj => obj.ObjectType).HasColumnName("object_type").IsRequired();
        builder.Property(obj => obj.Enable).HasColumnName("enable").IsRequired();
        builder.Property(obj => obj.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(obj => obj.Creator).HasColumnName("creator").IsRequired();
        builder.Property(obj => obj.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(obj => obj.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(obj => obj.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}
