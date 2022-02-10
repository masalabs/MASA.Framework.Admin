namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Entities
{
    public class DicValue
    {
        public Guid Id { get; set; }

        public Guid DicId { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public string Lable { get; set; } = default!;

        public string Value { get; set; } = default!;

        public int Sort { get; set; } = default;

        public string Description { get; set; } = default!;

        public bool Enable { get; set; } = true;
    }
}
