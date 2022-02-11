using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries
{
    public record DicPageQuery(DicPagingOptions Options) : Query<PagingResult<Domain.Entities.Dic>>
    {
        public override PagingResult<Domain.Entities.Dic> Result { get; set; }
    }
}
