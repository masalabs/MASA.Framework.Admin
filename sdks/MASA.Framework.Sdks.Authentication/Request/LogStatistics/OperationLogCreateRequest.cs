using System.ComponentModel.DataAnnotations;

namespace Masa.Framework.Sdks.Authentication.Request.LogStatistics
{
    public class OperationLogCreateRequest
    {
        [Required]
        public string Description { get; set; } = "";

        [Required]
        public OperationLogType OperationLogType { get; set; }
    }
}
