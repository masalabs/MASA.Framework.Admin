using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries
{
    public record DicValueQuery(Guid id) : Query<Domain.Entities.DicValue>
    {
        public override Domain.Entities.DicValue Result { get; set; } = default!;
    }
}
