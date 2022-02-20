using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MASA.Framework.Admin.Contracts.Logging
{
    [Table("operation_log")]
    public class OperationLog
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public OperationLogType Type { get; set; }

        public string ClientIP { get; set; }

        public int UserId { get; set; }
    }
}