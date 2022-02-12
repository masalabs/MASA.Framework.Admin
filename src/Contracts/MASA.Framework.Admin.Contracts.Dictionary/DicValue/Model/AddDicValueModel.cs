using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Framework.Admin.Contracts.Dictionary.DicValue.Model
{
    public class AddDicValueModel
    {
        [Required(ErrorMessage = "不能为空")]
        public Guid DicId { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public string Lable { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public string Value { get; set; }

        [Required(ErrorMessage = "不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "至少大于0")]
        public int Sort { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public string Description { get; set; }

        [Required(ErrorMessage = "不能为空")]
        public bool Enable { get; set; }
    }
}
