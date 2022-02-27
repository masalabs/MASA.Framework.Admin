namespace MASA.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class DepartmentUserEntityTypeConfiguration : IEntityTypeConfiguration<DepartmentUser>
    {
        public void Configure(EntityTypeBuilder<DepartmentUser> builder)
        {
            builder.ToTable("department_users", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(depart => depart.Id);
            builder.Property(depart => depart.Id).HasColumnName("id").IsRequired();

            builder.Property(depart => depart.UserId).HasColumnName("user_id").IsRequired();
        }
    }
}
