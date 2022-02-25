namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class DepartmentUser : Entity<Guid>
    {
        public Department Department { get; set; }

        public Guid UserId { get; set; }

        public string Position { get; set; } = "";

        public DepartmentUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
