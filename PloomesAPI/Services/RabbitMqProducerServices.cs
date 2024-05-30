using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace PloomesAPI.Services
{
    public class RabbitMqProducerServices
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;

        public RabbitMqProducerServices(IConfiguration config)
        {
            _config = config;

            var factory = new ConnectionFactory()
            {
                HostName = _config.GetSection("RabbitMq:Hostname")?.Value,
                UserName = _config.GetSection("RabbitMq:Username")?.Value,
                Password = _config.GetSection("RabbitMq:Password")?.Value,
                Port = 5672
            };

            _connection = factory.CreateConnection();
        }

        public void SendMessage(string message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "crud_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "crud_queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
