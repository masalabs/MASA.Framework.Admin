namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class ObjectItemEntityTypeConfiguration
    : IEntityTypeConfiguration<ObjectItem>
{
    public void Configure(EntityTypeBuilder<ObjectItem> builder)
    {
        builder.ToTable("permissions", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(c => c.Action).HasColumnName("action").HasMaxLength(20).IsRequired();
        builder.Property(c => c.ObjectIdentifies).HasColumnName("state").IsRequired();
        builder.Property(c => c.PermissionType).HasColumnName("permission_type").IsRequired();
    }
}
