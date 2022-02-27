namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class DepartmentUser : Entity<Guid>
    {
        public Department Department { get; private set; }

        public Guid UserId { get; private set; }

        public User User { get; set; }

        public string Position { get; private set; } = "";

        public DepartmentUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
