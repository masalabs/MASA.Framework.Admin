namespace Masa.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class UserGroupPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupPermission>
    {
        public void Configure(EntityTypeBuilder<UserGroupPermission> builder)
        {
            builder.ToTable("user_group_permissions", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ugp => ugp.Id);
            builder.Property(ugp => ugp.Id).HasColumnName("id").IsRequired();

            builder.Property(ugp => ugp.PermissionId).HasColumnName("permission_id").IsRequired();
        }
    }
}
