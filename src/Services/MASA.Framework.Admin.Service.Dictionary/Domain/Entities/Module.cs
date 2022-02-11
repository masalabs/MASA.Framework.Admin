using System.ComponentModel.DataAnnotations;

namespace MASA.Framework.Admin.Service.Dictionary.Domain.Entities
{
    public class Module
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
