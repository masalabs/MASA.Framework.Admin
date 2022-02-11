using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MASA.Framework.Admin.Models
{
    [Table("operation_log")]
    public class OperationLog
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}