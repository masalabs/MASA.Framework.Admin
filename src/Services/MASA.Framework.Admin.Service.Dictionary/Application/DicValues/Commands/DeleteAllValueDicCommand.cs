namespace MASA.Framework.Admin.Service.Dictionary.Application.DicValue.Commands
{
    public record DeleteAllDicValueCommand(List<Guid> ids) : Command
    {

    }
}
