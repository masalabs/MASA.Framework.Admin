namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands
{
    public record UpdateCommand(Infrastructure.Entities.Dic Dic) : Command
    {
        public Infrastructure.Entities.Dic Result { get; set; } = default!;
    }
}
