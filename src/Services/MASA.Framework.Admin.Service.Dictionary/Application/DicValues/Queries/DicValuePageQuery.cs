using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Options;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValues.Queries
{
    public record DicValuePageQuery(DicValuePagingOptions Options) : Query<PagingResult<Domain.Entities.DicValue>>
    {
        public override PagingResult<Domain.Entities.DicValue> Result { get; set; } = default!;
    }
}
