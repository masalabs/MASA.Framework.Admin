using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.OperationLogs.Commands
{
    public record CommandBase : Command
    {
        public Guid Creator { get; set; }
    }
}
