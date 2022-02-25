namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_groups", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ug => ug.Id);
            builder.HasIndex(ug => ug.Code);
            builder.Property(ug => ug.Id).HasColumnName("id").IsRequired();

            builder.HasMany(ug => ug.UserGroupItems).WithOne(ugi => ugi.UserGroup);
        }
    }
}
