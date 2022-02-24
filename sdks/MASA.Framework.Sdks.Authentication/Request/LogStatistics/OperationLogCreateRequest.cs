using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Sdks.Authentication.Request.LogStatistics
{
    public class OperationLogCreateRequest
    {
        [Required]
        public string Description { get; set; } = "";

        [Required]
        public OperationLogType OperationLogType { get; set; }
    }
}
