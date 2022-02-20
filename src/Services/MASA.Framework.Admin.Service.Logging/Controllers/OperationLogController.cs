using Dapr;
using Dapr.Client;
using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Repositories;
using MASA.Framework.Admin.Service.Logging.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Service.Logging.Controllers
{
    //TODO:[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OperationLogController : ControllerBase
    {
        private readonly IOperationLogRepository _repository;
        private readonly DaprClient _daprClient;

        public OperationLogController(IOperationLogRepository repository, DaprClient daprClient)
        {
            _repository = repository;
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] OperationLogQueryViewModel viewModel)
        {
            var operationLogs = _repository
                .GetAll();

            //TODO:Get userId from identity
            var userId = 1;
            operationLogs = operationLogs.Where(log => log.UserId == userId);

            var count = await operationLogs.CountAsync();

            if (viewModel.Description != null)
            {
                operationLogs = operationLogs.Where(log => log.Description.Contains(viewModel.Description));
            }

            operationLogs = operationLogs.OrderByDescending(log => log.CreateTime);

            operationLogs = operationLogs.Skip(viewModel.Offset).Take(viewModel.Limit);

            var data = operationLogs.Select(log => new OperationLogDto
            {
                Id = log.Id,
                CreateTime = log.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Description = log.Description,
                //TODO:Username
            });

            var result = new PageResult<OperationLogDto>(count, data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OperationLogViewModel viewModel)
        {
            var operationLog = new OperationLog
            {
                Description = viewModel.Description,
                CreateTime = DateTime.Now,
                UserId = 1,//TODO:UserId
                ClientIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                Type = viewModel.OperationLogType.Value
            };
            await _daprClient.PublishEventAsync("rabbitmq-pubsub", "operation_log", operationLog);

            return Ok();
        }
    }
}