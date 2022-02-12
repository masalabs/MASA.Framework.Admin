using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries
{
    public record DicValuePageQuery(DicValuePagingOptions Options) : Query<PagingResult<DicValueViewModel>>
    {
        public override PagingResult<DicValueViewModel> Result { get; set; } = default!;
    }
}
