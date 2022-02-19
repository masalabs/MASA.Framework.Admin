namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RoleItemEntityTypeConfiguration
    : IEntityTypeConfiguration<RoleItem>
{
    public void Configure(EntityTypeBuilder<RoleItem> builder)
    {
        builder.ToTable("role_items", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(rolteItem => rolteItem.Id);
        builder.Property(rolteItem => rolteItem.Id).HasColumnName("id").IsRequired();

        builder.Property(rolteItem => rolteItem.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(rolteItem => rolteItem.ParentRoleId).HasColumnName("parent_role_id").IsRequired();
    }
}
