namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations;

public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles", UserDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.RoleId).HasColumnName("role_id").IsRequired();
    }
}

