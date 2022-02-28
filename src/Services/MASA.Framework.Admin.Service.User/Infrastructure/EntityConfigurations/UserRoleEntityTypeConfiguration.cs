namespace Masa.Framework.Admin.Service.User.Infrastructure.EntityConfigurations;

public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles", UserDbContext.DEFAULT_SCHEMA);

        builder.HasKey(userRole => userRole.Id);
        builder.Property(userRole => userRole.Id).HasColumnName("id").IsRequired();

        builder.Property(userRole => userRole.RoleId).HasColumnName("role_id").IsRequired();
    }
}

