using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel
{
    public class DicValueViewModel
    {
        public Guid Id { get; set; }

        public Guid DicId { get; set; }

        public string Lable { get; set; }

        public string Value { get; set; }

        public int Sort { get; set; }

        public string Description { get; set; }

        public bool Enable { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
