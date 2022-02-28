namespace Masa.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_groups", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ug => ug.Id);
            builder.HasIndex(ug => ug.Code);
            builder.Property(ug => ug.Id).HasColumnName("id").IsRequired();

            builder.Property(ug => ug.Code).HasColumnName("code").IsRequired();
            builder.Property(ug => ug.Name).HasColumnName("name").IsRequired();
            builder.Property(ug => ug.Describtion).HasColumnName("describtion");

            builder.Property(depart => depart.IsDeleted).HasColumnName("is_deleted").IsRequired();
            builder.Property(depart => depart.Creator).HasColumnName("creator").IsRequired();
            builder.Property(depart => depart.CreationTime).HasColumnName("creation_time").IsRequired();
            builder.Property(depart => depart.Modifier).HasColumnName("modifier").IsRequired();
            builder.Property(depart => depart.ModificationTime).HasColumnName("modifier_time").IsRequired();

            builder.HasMany(ug => ug.UserGroupItems).WithOne(ugi => ugi.UserGroup);
        }
    }
}
