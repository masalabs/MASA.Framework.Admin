using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Login.Model
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Account { get; set; } = "";

        public string NickName { get; set; } = "";

        public int State { get; set; }
    }
}
