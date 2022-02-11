namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregate.User>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregate.User> builder)
    {
        builder.ToTable("users", UserDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.Account).HasColumnName("account").HasMaxLength(20).IsRequired();
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(30);
        builder.Property(c => c.Password).HasColumnName("password").HasMaxLength(50).IsRequired();
        builder.Property(c => c.Salt).HasColumnName("salt").HasMaxLength(6).IsRequired();
        builder.Property(c => c.Gender).HasColumnName("gender").IsRequired();
        builder.Property(c => c.Cover).HasColumnName("cover").HasMaxLength(200);
        builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(100);
        builder.Property(c => c.State).HasColumnName("state").IsRequired();
        builder.Property(c => c.LastLoginTime).HasColumnName("last_login_time");
        builder.Property(c => c.LastUpdateTime).HasColumnName("last_update_time");

        builder.Property(c => c.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(c => c.Creator).HasColumnName("creator").IsRequired();
        builder.Property(c => c.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(c => c.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(c => c.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}

