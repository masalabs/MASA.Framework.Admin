using MASA.Contrib.Dispatcher.Events.Enums;

namespace MASA.Framework.Admin.Services.BackgroundJobs.Application
{
    public class JobCommandHandler
    {
        private readonly IJobRepository _jobRepository;
        public JobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [EventHandler(Order = 1)]
        public async Task CreateHandleAsync(JobCreateCommand command)
        {
            var entity = new Job
            {
                Id = Guid.NewGuid(),
                JobName = command.Name,
                JobMethod = command.Method,
                JobArgs = command.Args,
                IsStop = command.IsStop,
                PeriodSeconds = command.PeriodSeconds
            };
            await _jobRepository.InsertAsync(entity);
        }

        public async Task UpdateHandleAsync(JobUpdateCommand command)
        {
            var entity = await _jobRepository.FindAsync(command.Id);

            entity.JobName = command.Name;
            entity.JobMethod = command.Method;
            entity.JobArgs = command.Args;
            entity.IsStop = command.IsStop;
            entity.PeriodSeconds = command.PeriodSeconds;
            entity.UpdateTime = DateTimeOffset.Now;

            await _jobRepository.InsertAsync(entity);
        }
    }
}