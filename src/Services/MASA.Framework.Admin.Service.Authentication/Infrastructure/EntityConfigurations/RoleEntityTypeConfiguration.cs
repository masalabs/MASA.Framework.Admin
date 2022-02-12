namespace MASA.Framework.Admin.Service.Authentication.Infrastructure.EntityConfigurations;

public class RoleEntityTypeConfiguration
    : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", AuthenticationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(10).IsRequired();
        builder.Property(c => c.Describe).HasColumnName("describe").IsRequired();
        builder.Property(c => c.Number).HasColumnName("number").IsRequired();
        builder.Property(c => c.State).HasColumnName("state").IsRequired();
        builder.Property(c => c.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(c => c.Creator).HasColumnName("creator").IsRequired();
        builder.Property(c => c.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(c => c.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(c => c.ModificationTime).HasColumnName("modifier_time").IsRequired();

        builder.HasMany(express => express.Permissions).WithOne(p => p.Role);
        builder.HasMany(express => express.RoleItems).WithOne(p => p.Role);
    }
}
