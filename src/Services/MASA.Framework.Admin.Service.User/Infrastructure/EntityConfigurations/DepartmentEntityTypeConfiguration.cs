namespace Masa.Framework.Admin.Service.User.Infrastructure.EntityConfigurations
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("department", UserDbContext.DEFAULT_SCHEMA);

            builder.HasKey(depart => depart.Id);
            builder.HasIndex(depart => depart.Code);
            builder.Property(depart => depart.Id).HasColumnName("id").IsRequired();

            builder.Property(depart => depart.Code).HasColumnName("code").IsRequired();
            builder.Property(depart => depart.Name).HasColumnName("name").IsRequired();
            builder.Property(depart => depart.Describtion).HasColumnName("describtion");
            builder.Property(depart => depart.ParentId).HasColumnName("parent_id");

            builder.Property(depart => depart.IsDeleted).HasColumnName("is_deleted").IsRequired();
            builder.Property(depart => depart.Creator).HasColumnName("creator").IsRequired();
            builder.Property(depart => depart.CreationTime).HasColumnName("creation_time").IsRequired();
            builder.Property(depart => depart.Modifier).HasColumnName("modifier").IsRequired();
            builder.Property(depart => depart.ModificationTime).HasColumnName("modifier_time").IsRequired();

            builder.HasMany(depart => depart.DepartmentUsers).WithOne(du => du.Department);

        }
    }
}
