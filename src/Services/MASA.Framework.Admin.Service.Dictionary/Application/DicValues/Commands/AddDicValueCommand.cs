using MASA.Framework.Admin.Contracts.Dictionary.DicValue.Model;

namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands
{
    public record AddDicValueCommand(AddDicValueModel Model) : Command
    {
        public Guid Result { get; set; } = default!;
    }
}
