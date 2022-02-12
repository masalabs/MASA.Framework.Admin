using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries
{
    public record DicValueQuery(Guid id) : Query<DicValueViewModel>
    {
        public override DicValueViewModel Result { get; set; } = default!;
    }
}
