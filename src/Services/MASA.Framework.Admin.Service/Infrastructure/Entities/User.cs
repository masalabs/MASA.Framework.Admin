using MASA.BuildingBlocks.DDD.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MASA.Framework.Admin.Service.Login.Infrastructure.Entities
{
    [Table("Users")]
    public class User : AuditAggregateRoot<int, Guid>
    {
        [Comment("Account")]
        [Required(ErrorMessage = "Account is required")]
        [Column(TypeName = "nvarchar(16)")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "Account length range is [2-16]")]
        public string Account { get; set; } = "";

        [Comment("Password")]
        [Required(ErrorMessage = "Password is required")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Password length range is [2-100]")]
        public string Password { get; set; } = "";

        [Comment("NickName")]
        [Required(ErrorMessage = "NickName is required")]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "NickName length range is [2-100]")]
        public string NickName { get; set; } = "";

        [Comment("Salt")]
        [Required(ErrorMessage = "Salt is required")]
        [Column(TypeName = "nvarchar(6)")]
        [StringLength(6)]
        public string Salt { get; set; } = default!;

        [Comment("State")]
        [Range(1, int.MaxValue, ErrorMessage = "State is required")]
        [Column(TypeName = "tinyint")]
        public int State { get; set; }
    }
}
