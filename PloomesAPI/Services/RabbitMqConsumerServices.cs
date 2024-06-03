using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace PloomesAPI.Services
{
    public class RabbitMqConsumerServices
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumerServices(IConfiguration config)
        {
            _config = config;

            var factory = new ConnectionFactory()
            {
                HostName = _config.GetSection("RabbitMq:Hostname")?.Value,
                UserName = _config.GetSection("RabbitMq:Username")?.Value,
                Password = _config.GetSection("RabbitMq:Password")?.Value
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartConsumer(string queue)
        {
            _channel.QueueDeclare(queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Aqui você pode adicionar a lógica para processar a mensagem
                Console.WriteLine("Received message: {0}", message);
            };

            _channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);

        }
    }
}
