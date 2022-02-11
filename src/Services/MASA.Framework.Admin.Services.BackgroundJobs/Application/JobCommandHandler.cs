using MASA.Contrib.Dispatcher.Events.Enums;

namespace MASA.Framework.Admin.Services.BackgroundJobs.Application
{
    public class JobCommandHandler
    {
        public JobCommandHandler()
        {

        }

        [EventHandler(Order = 1)]
        public async Task CreateHandleAsync(JobCreateCommand command)
        {
            //you work
            await Task.CompletedTask;
        }
    }

    public class OrderStockHandler : CommandHandler<JobCreateCommand>
    {
        public override Task CancelAsync(JobCreateCommand comman)
        {
            //cancel todo callback 
            return Task.CompletedTask;
        }

        [EventHandler(FailureLevels = FailureLevels.ThrowAndCancel)]
        public override Task HandleAsync(JobCreateCommand comman)
        {
            //todo decrease stock
            return Task.CompletedTask;
        }

        [EventHandler(0, FailureLevels.Ignore, IsCancel = true)]
        public Task AddCancelLogs(JobCreateCommand query)
        {
            //todo increase stock
            return Task.CompletedTask;
        }
    }
}