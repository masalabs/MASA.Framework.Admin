namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class PermissionEntityTypeConfiguration
    : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permission", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(permission => permission.Id);
        builder.Property(permission => permission.Id).HasColumnName("id").IsRequired();

        builder.Property(permission => permission.ObjectType).HasColumnName("object_type").IsRequired();
        builder.Property(permission => permission.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(permission => permission.Resource).HasColumnName("resource").HasMaxLength(30).IsRequired();
        builder.Property(permission => permission.Scope).HasColumnName("scope").HasMaxLength(50).IsRequired();
        builder.Property(permission => permission.Action).HasColumnName("action").HasMaxLength(100).IsRequired();
        builder.Property(permission => permission.Code).HasColumnName("code").HasMaxLength(400).IsRequired();
        builder.Property(permission => permission.Enable).HasColumnName("enable").IsRequired();
        builder.Property(permission => permission.PermissionType).HasColumnName("permission_type").IsRequired();

        builder.Property(permission => permission.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(permission => permission.Creator).HasColumnName("creator").IsRequired();
        builder.Property(permission => permission.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(permission => permission.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(permission => permission.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}
