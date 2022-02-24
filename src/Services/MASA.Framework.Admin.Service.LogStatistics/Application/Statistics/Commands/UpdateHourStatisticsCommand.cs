using MASA.Contrib.ReadWriteSpliting.CQRS.Commands;

namespace MASA.Framework.Admin.Service.LogStatistics.Application.Statistics.Commands
{
    public record UpdateHourStatisticsCommand(int UV, int PV, int IpCount) : Command
    {
    }
}
