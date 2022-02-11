namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RoleItemEntityTypeConfiguration
    : IEntityTypeConfiguration<RoleItem>
{
    public void Configure(EntityTypeBuilder<RoleItem> builder)
    {
        builder.ToTable("role_items", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.ChildrenRoleId).HasColumnName("children_role_id").IsRequired();
    }
}
