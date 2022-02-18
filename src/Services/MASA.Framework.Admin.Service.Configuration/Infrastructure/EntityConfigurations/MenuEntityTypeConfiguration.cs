namespace MASA.Framework.Admin.Configuration.Infrastructure.EntityConfigurations;

public class MenuEntityTypeConfiguration
    : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("menus", ConfigurationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").IsRequired();

        builder.Property(c => c.Code).HasColumnName("code").HasMaxLength(20).IsRequired();
        builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        builder.Property(c => c.Describe).HasColumnName("describe");
        builder.Property(c => c.Icon).HasColumnName("icon").HasMaxLength(100);
        builder.Property(c => c.ParentId).HasColumnName("parent_id");
        builder.Property(c => c.ParentName).HasColumnName("parent_name").HasMaxLength(200);
        builder.Property(c => c.Url).HasColumnName("url").HasMaxLength(200).IsRequired();
        builder.Property(c => c.Sort).HasColumnName("sort").IsRequired();
        builder.Property(c => c.State).HasColumnName("state").IsRequired();

        builder.Property(c => c.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(c => c.Creator).HasColumnName("creator").IsRequired();
        builder.Property(c => c.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(c => c.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(c => c.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}
