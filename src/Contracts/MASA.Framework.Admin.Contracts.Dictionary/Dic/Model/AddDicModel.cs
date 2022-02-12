using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.Dic.Model
{
    public class AddDicModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public bool Enable { get; set; }

        public Guid ModuleId { get; set; }
    }
}
