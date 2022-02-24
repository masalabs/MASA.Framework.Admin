namespace MASA.Framework.Sdks.Authentication.Response.LogStatistics
{
    public class StatisticsQueryResponse
    {
        public Guid Id { get; set; }

        public int PV { get; set; }

        public int UV { get; set; }

        public int IPCount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
