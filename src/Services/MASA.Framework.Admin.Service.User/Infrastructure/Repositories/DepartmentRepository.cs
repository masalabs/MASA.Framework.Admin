namespace MASA.Framework.Admin.Service.User.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<UserDbContext, Department>, IDepartmentRepository
    {
        public DepartmentRepository(UserDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
