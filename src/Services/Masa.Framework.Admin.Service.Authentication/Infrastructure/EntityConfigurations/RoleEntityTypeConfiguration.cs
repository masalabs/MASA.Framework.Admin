namespace Masa.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RoleEntityTypeConfiguration
    : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(role => role.Id);
        builder.Property(role => role.Id).HasColumnName("id").IsRequired();

        builder.Property(role => role.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(role => role.Describe).HasColumnName("describe").IsRequired();
        builder.Property(role => role.Number).HasColumnName("number").IsRequired();
        builder.Property(role => role.Enable).HasColumnName("enable").IsRequired();
        builder.Property(role => role.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(role => role.Creator).HasColumnName("creator").IsRequired();
        builder.Property(role => role.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(role => role.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(role => role.ModificationTime).HasColumnName("modifier_time").IsRequired();

        builder.HasMany(role => role.Permissions).WithOne(rolePermission => rolePermission.Role).HasForeignKey("role_id");
        builder.HasMany(role => role.RoleItems).WithOne(roleItem => roleItem.Role).HasForeignKey("parent_role_id");
    }
}
