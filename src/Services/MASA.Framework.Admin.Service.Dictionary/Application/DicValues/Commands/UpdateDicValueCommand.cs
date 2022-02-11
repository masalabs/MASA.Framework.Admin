namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands
{
    public record UpdateDicValueCommand(Domain.Entities.DicValue DicValue) : Command
    {
        public Domain.Entities.DicValue Result { get; set; } = default!;
    }
}
