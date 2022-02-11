using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MASA.Framework.Admin.Infrastructure
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly RabbitMQEventBusOptions _options;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQEventBus(IOptions<RabbitMQEventBusOptions> optionsAccesser)
        {
            _options = optionsAccesser.Value;
            Initialize();
        }

        private void Initialize()
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
        }

        public void Publish(object model)
        {
            var message = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: _options.QueueName, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
