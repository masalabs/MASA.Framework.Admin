namespace MASA.Framework.Admin.Service.Dictionary.Application.Dic.Commands
{
    public record AddCommand(Infrastructure.Entities.Dic Dic) : Command
    {
        public Infrastructure.Entities.Dic Result { get; set; } = default!;
    }
}
