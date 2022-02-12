using MASA.Contrib.ReadWriteSpliting.CQRS.Queries;
using MASA.Framework.Admin.Contracts.Dictionary;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.Options;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Queries
{
    public record DicPageQuery(DicPagingOptions Options) : Query<PagingResult<DicViewModel>>
    {
        public override PagingResult<DicViewModel> Result { get; set; }
    }
}
