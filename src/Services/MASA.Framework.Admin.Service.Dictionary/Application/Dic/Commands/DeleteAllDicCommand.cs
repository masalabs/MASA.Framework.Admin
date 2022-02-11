namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands
{
    public record DeleteAllDicCommand(List<Guid> ids) : Command
    {

    }
}
