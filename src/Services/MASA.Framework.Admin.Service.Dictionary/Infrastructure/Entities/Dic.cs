namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Entities
{
    public class Dic
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public string Name { get; set; } = default!;

        public string Type { get; set; } = default!;

        public string Description { get; set; } = default!;

        public bool Enable { get; set; } = true;

        public Guid ModuleId { get; set; }
    }
}
