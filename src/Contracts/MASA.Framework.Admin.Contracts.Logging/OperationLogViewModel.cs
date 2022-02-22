using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
