using MASA.Framework.Admin.Infrastructure;
using MASA.Framework.Admin.Models;
using MASA.Framework.Admin.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MASA.Framework.Admin.Consumer
{
    public class RabbitMQConsumerHostedService : IHostedService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitMQEventBusOptions _options;

        public RabbitMQConsumerHostedService(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQEventBusOptions> optionsAccesser)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _options = optionsAccesser.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password
            };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
              queue: _options.QueueName,
              durable: true,
              exclusive: false,
              autoDelete: false,
              arguments: null
               );

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, e) =>
            {
                var message = e.Body.ToArray();
                var operationLog = JsonConvert.DeserializeObject<OperationLog>(Encoding.UTF8.GetString(message));

                using var scope = _serviceScopeFactory.CreateScope();
                using var repository = scope.ServiceProvider.GetService<IOperationLogRepository>();

                var success = repository.Create(operationLog);
                if (success)
                {
                    _channel.BasicAck(e.DeliveryTag, false);
                }
            };

            _channel.BasicConsume(queue: _options.QueueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Dispose();
            _connection.Dispose();

            return Task.CompletedTask;
        }
    }
}
