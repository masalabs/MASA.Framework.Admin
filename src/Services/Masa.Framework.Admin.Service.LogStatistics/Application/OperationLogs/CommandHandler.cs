namespace Masa.Framework.Admin.Service.LogStatistics.Application.OperationLogs;

public class CommandHandler
{
    readonly IOperationLogRepository _operationLogRepository;

    public CommandHandler(IOperationLogRepository operationLogRepository)
    {
        _operationLogRepository = operationLogRepository;
    }

    [EventHandler]
    public async Task CreateAsync(CreateCommand createCommand)
    {
        var createRequest = createCommand.OperationLogCreateRequest;
        await _operationLogRepository.AddAsync(new OperationLog(createCommand.Creator, createRequest.OperationLogType, createRequest.Description));
    }
}

