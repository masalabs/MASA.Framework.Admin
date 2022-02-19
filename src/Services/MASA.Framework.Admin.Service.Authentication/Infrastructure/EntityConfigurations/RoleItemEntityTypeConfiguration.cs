namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RoleItemEntityTypeConfiguration
    : IEntityTypeConfiguration<RoleItem>
{
    public void Configure(EntityTypeBuilder<RoleItem> builder)
    {
        builder.ToTable("role_items", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(roleItem => roleItem.Id);
        builder.Property(roleItem => roleItem.Id).HasColumnName("id").IsRequired();

        builder.Property(roleItem => roleItem.ParentRoleId).HasColumnName("parent_role_id").IsRequired();
        builder.Property(roleItem => roleItem.RoleId).HasColumnName("role_id").IsRequired();
    }
}
