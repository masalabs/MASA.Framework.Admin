using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Login.Model
{
    public class LoginViewModel
    {
        public int Code { get; set; }

        public string Result { get; set; } = "";
    }
}
