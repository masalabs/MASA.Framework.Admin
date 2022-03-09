namespace Masa.Framework.Admin.Service.LogStatistics.Domain.Aggregates;

public class VisitStatisticsRecord : AggregateRoot<Guid>
{
    public int PV { get; private set; }

    public int UV { get; private set; }

    public int IPCount { get; private set; }

    public DateTime DateTime { get; private set; }

    public VisitStatisticType Type { get; private set; }

    private VisitStatisticsRecord()
    {

    }

    public VisitStatisticsRecord(int pv, int uv, int ipCount, VisitStatisticType type, DateTime dateTime)
    {
        PV = pv;
        UV = uv;
        IPCount = ipCount;
        Type = type;
        DateTime = dateTime;
    }

    public void Update(int pv, int uv, int ipCount)
    {
        PV = pv;
        UV = uv;
        IPCount = ipCount;
    }

}

