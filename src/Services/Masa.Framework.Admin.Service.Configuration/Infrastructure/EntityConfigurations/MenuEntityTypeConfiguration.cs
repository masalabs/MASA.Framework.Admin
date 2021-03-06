namespace Masa.Framework.Admin.Configuration.Infrastructure.EntityConfigurations;

public class MenuEntityTypeConfiguration
    : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("menus", ConfigurationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(menu => menu.Id);
        builder.Property(menu => menu.Id).HasColumnName("id").IsRequired();

        builder.Property(menu => menu.Code).HasColumnName("code").HasMaxLength(20).IsRequired();
        builder.Property(menu => menu.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        builder.Property(menu => menu.Description).HasColumnName("description");
        builder.Property(menu => menu.Icon).HasColumnName("icon").HasMaxLength(100).IsRequired();
        builder.Property(menu => menu.ParentId).HasColumnName("parent_id");
        builder.Property(menu => menu.Url).HasColumnName("url").HasMaxLength(200).IsRequired();
        builder.Property(menu => menu.Sort).HasColumnName("sort").IsRequired();
        builder.Property(menu => menu.Enabled).HasColumnName("enabled").IsRequired();
        builder.Property(menu => menu.OnlyJump).HasColumnName("onlyJump").IsRequired();

        builder.Property(menu => menu.IsDeleted).HasColumnName("is_deleted").IsRequired();
        builder.Property(menu => menu.Creator).HasColumnName("creator").IsRequired();
        builder.Property(menu => menu.CreationTime).HasColumnName("creation_time").IsRequired();
        builder.Property(menu => menu.Modifier).HasColumnName("modifier").IsRequired();
        builder.Property(menu => menu.ModificationTime).HasColumnName("modifier_time").IsRequired();
    }
}
