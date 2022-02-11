using MASA.Framework.Admin.Infrastructure;
using MASA.Framework.Admin.Models;
using MASA.Framework.Admin.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MASA.Framework.Admin.Api.Controllers
{
    //TODO:[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OperationLogController : ControllerBase
    {
        private readonly IOperationLogRepository _repository;
        private readonly IEventBus _eventBus;

        public OperationLogController(IOperationLogRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
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
                Username = log.User.Username
            });

            var result = new PageResult<OperationLogDto>(count, data);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OperationLogViewModel viewModel)
        {
            var operationLog = new OperationLog
            {
                Description = viewModel.Description,
                CreateTime = DateTime.Now,
                UserId = 1//TODO:UserId
            };
            _eventBus.Publish(operationLog);

            return Ok();
        }
    }
}