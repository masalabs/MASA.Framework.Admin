using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries
{
    public record DicQuery(Guid Id) : Query<DicViewModel>
    {
        public override DicViewModel Result { get; set; } = default!;
    }
}
