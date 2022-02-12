namespace MASA.Framework.Admin.Services.BackgroundJobs.Infrastructure
{
    public class BackgroundJobService : IHostedService,IBackgroundWorker, IDisposable
    {
        protected IBackgroundWorker BackgroundJobWorker { get; set; }

        public BackgroundJobService(IBackgroundWorker backgroundJobWorker)
        {
            BackgroundJobWorker = backgroundJobWorker;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return BackgroundJobWorker.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return BackgroundJobWorker.StopAsync(cancellationToken);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
