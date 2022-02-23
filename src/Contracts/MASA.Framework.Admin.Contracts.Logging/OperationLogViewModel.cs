using MASA.Framework.Admin.Contracts.Base.Enum;
using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Contracts.Logging
{
    public class OperationLogViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public OperationLogType? OperationLogType { get; set; }
    }
}
