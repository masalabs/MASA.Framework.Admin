namespace MASA.Framework.Admin.Service.Dictionary.Infrastructure.Entities
{
    public class Dictionary
    {
        public int Id { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public string Name { get; set; } = default!;

        public string Key { get; set; } = default!;

        public string Value { get; set; } = default!;

        public string Description { get; set; } = default!;

    }
}
