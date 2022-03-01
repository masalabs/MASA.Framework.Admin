using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Masa.Framework.Admin.Service.User.Domain.Aggregates
{
    public class DepartmentUser : Entity<Guid>
    {
        public Guid DepartmentId { get; private set; }

        private Department _department;

        public virtual Department Department { get => LazyLoader.Load(this, ref _department); }

        public Guid UserId { get; private set; }

        public virtual User User { get; }

        public string Position { get; private set; } = "";

        private ILazyLoader LazyLoader { get; set; }

        private DepartmentUser(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public DepartmentUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
