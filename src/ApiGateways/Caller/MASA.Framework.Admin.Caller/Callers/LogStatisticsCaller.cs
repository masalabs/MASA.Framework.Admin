namespace MASA.Framework.Admin.Caller.Callers
{
    public class LogStatisticsCaller : HttpClientCallerBase
    {
        protected override string BaseAddress { get; set; }


        public LogStatisticsCaller(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Name = nameof(LogStatisticsCaller);
            BaseAddress = "";
        }
    }
}
