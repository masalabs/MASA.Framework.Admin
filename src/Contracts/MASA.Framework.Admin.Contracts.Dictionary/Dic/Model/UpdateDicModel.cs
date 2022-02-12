using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.Dic.Model
{
    public class UpdateDicModel
    {
        [Required(ErrorMessage = "不能为空")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "不能为空")]

        public string Type { get; set; }

        [Required(ErrorMessage = "不能为空")]

        public string Description { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public bool Enable { get; set; }

        public Guid ModuleId { get; set; }
    }
}
