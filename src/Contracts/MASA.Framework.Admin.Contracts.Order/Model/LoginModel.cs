using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Order.Model
{
    public class LoginModel
    {
        [Required]
        public string Account { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
