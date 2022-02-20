using Dapr;
using MASA.Framework.Admin.Contracts.Logging;
using MASA.Framework.Admin.Service.Logging.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MASA.Framework.Admin.Service.PageviewStatistics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperationLogController : ControllerBase
    {
        private readonly IOperationLogRepository _repository;

        public OperationLogController(IOperationLogRepository repository)
        {
            _repository = repository;
        }

        [Topic("rabbitmq-pubsub", "operation_log")]
        [HttpPost("create")]
        public IActionResult CreateOperationLog([FromBody] OperationLog operationLog)
        {
            _repository.Create(operationLog);
            return Ok();
        }
    }
}