namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.User>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.User> builder)
    {
        builder.ToTable("users", UserDbContext.DEFAULT_SCHEMA);

        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("id").IsRequired();

        builder.Property(user => user.Account).HasColumnName("account").HasMaxLength(20).IsRequired();
        builder.Property(user => user.Name).HasColumnName("name").HasMaxLength(30);
        builder.Property(user => user.Password).HasColumnName("password").HasMaxLength(50).IsRequired();
        builder.Property(user => user.Salt).HasColumnName("salt").HasMaxLength(6).IsRequired();
        builder.Property(user => user.Gender).HasColumnName("gender").IsRequired();
        builder.Property(user => user.Cover).HasColumnName("cover").HasMaxLength(200);
        builder.Property(user => user.Email).HasColumnName("email").HasMaxLength(100);
        builder.Property(user => user.Enable).HasColumnName("enable").IsRequired();
        builder.Property(user => user.LastLoginTime).HasColumnName("last_login_time");
        builder.Property(user => user.LastUpdateTime).HasColumnName("last_update_time");

        builder.Property(user => user.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(user => user.Creator).HasColumnName("creator").IsRequired();
        builder.Property(user => user.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(user => user.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(user => user.ModificationTime).HasColumnName("modifier_time").IsRequired();

        builder.HasMany(user => user.UserRoles).WithOne(userRole => userRole.User);
    }
}

