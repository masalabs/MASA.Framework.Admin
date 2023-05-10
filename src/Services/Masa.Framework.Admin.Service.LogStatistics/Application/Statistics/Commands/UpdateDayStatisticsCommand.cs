using Masa.Contrib.ReadWriteSplitting.Cqrs.Commands;

namespace Masa.Framework.Admin.Service.LogStatistics.Application.Statistics.Commands
{
    public record UpdateDayStatisticsCommand(int UV, int PV, int IpCount) : Command
    {
    }
}
