using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Logging
{
    public class OperationLogDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string CreateTime { get; set; }

        public string Username { get; set; }
    }
}
