using MASA.Framework.Admin.Contracts.Base.Enum;
using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Contracts.Base.Request
{
    public class OperationLogCreateRequest
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public OperationLogType OperationLogType { get; set; }
    }
}
