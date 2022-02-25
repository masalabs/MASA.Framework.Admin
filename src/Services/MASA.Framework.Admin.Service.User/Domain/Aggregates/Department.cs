namespace MASA.Framework.Admin.Service.User.Domain.Aggregates
{
    public class Department : AuditAggregateRoot<Guid, Guid>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Describtion { get; set; }

        public Guid? ParentId { get; set; }

        private List<DepartmentUser> departmentUsers = new List<DepartmentUser>();

        public IReadOnlyCollection<DepartmentUser> DepartmentUsers => departmentUsers;

        private Department()
        {

        }

        public Department(string name, string code, string describtion)
        {
            Name = name;
            Code = code;
            Describtion = describtion;
        }
    }
}
