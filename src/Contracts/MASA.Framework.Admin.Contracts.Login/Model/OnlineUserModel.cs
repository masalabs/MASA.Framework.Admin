using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Login.Model
{
    public class OnlineUserModel
    {
        public int UserId { get; set; }

        public string ConnectionId { get; set; } = "";

        public string Account { get; set; } = "";

        public string NickName { get; set; } = "";

        public DateTime LoginTime { get; set; }

        public string LoginIP { get; set; } = "";
    }
}
