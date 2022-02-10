namespace MASA.Framework.Admin.Service.Infrastructure.Entities
{
    public class Order1
    {
        public int Id { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public string OrderNumber { get; set; } = default!;

        public string Address { get; set; } = default!;
    }
}