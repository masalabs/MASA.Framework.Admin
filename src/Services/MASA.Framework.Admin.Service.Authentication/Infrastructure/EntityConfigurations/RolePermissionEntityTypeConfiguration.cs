namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RolePermissionEntityTypeConfiguration
    : IEntityTypeConfiguration<RolePermission>
{

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("role_permission", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(rolePermission => rolePermission.Id);
        builder.Property(rolePermission => rolePermission.Id).HasColumnName("id").IsRequired();

        builder.Property(rolePermission => rolePermission.PermissionsId).HasColumnName("permissions_id").IsRequired();
    }
}
