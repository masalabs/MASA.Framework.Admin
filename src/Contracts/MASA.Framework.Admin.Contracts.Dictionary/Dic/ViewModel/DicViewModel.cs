using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel
{
    public class DicViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public bool Enable { get; set; }

        public Guid ModuleId { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
