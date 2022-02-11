using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Models
{
    public class OperationLogViewModel
    {
        [Required]
        public string Description { get; set; }
    }
}
