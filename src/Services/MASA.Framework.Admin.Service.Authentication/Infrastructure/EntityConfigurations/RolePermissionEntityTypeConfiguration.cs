namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RolePermissionEntityTypeConfiguration
    : IEntityTypeConfiguration<RolePermission>
{

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permission", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.PermissionsId).HasColumnName("permissions_id").IsRequired();
        builder.Property(c => c.PermissionEffect).HasColumnName("permission_effect").IsRequired();
    }
}
