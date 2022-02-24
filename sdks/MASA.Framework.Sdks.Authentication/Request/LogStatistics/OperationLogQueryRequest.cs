using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Sdks.Authentication.Request.LogStatistics
{
    public class OperationLogQueryRequest
    {
        [Range(0, int.MaxValue)]
        public int Offset { get; set; } = 0;

        [Range(1, int.MaxValue)]
        public int Limit { get; set; } = 10;

        public string Description { get; set; }
    }
}
