using MASA.Framework.Admin.Contracts.Dictionary.Dic.Model;
using MASA.Framework.Admin.Contracts.Dictionary.Dic.ViewModel;

namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands
{
    public record UpdateDicCommand(UpdateDicModel Model) : Command
    {
        public Guid Result { get; set; } = default!;
    }
}
