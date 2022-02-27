namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class UserGroupItemEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupItem>
    {
        public void Configure(EntityTypeBuilder<UserGroupItem> builder)
        {
            builder.ToTable("user_group_items", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ug => ug.Id);
            builder.Property(ug => ug.Id).HasColumnName("id").IsRequired();

            builder.Property(ug => ug.UserId).HasColumnName("user_id").IsRequired();
        }
    }
}
